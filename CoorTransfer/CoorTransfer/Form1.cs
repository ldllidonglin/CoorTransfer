using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<String[]> data;  // 存储csv的数据
        public class CSVUtil
        {
            //write a new file, existed file will be overwritten
            public static void WriteCSV(string filePathName, List<String[]> ls)
            {
                WriteCSV(filePathName, false, ls);
            }
            //write a file, existed file will be overwritten if append = false
            public static void WriteCSV(string filePathName, bool append, List<String[]> ls)
            {
                StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
                foreach (String[] strArr in ls)
                {
                    fileWriter.WriteLine(String.Join(",", strArr));
                }
                fileWriter.Flush();
                fileWriter.Close();

            }
            public static List<String[]> ReadCSV(string filePathName)
            {
                List<String[]> ls = new List<String[]>();
                StreamReader fileReader = new StreamReader(filePathName, UnicodeEncoding.GetEncoding("gb2312"));
                string strLine = "";
                while (strLine != null)
                {
                    strLine = fileReader.ReadLine();
                    if (strLine != null && strLine.Length > 0)
                    {
                        ls.Add(strLine.Split(','));
                        System.Diagnostics.Debug.WriteLine(strLine);
                    }
                }
                fileReader.Close();
                return ls;
            }

        }
        public class GPS
        {
            private double PI = 3.141592653589793;
            private double x_pi = 3.141592653589793 * 3000.0 / 180.0;
            //WGS-84 to GCJ-02
            public Dictionary<string, double> gcj_encrypt(double wgsLat, double wgsLon)
            {
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                if (outOfChina(wgsLat, wgsLon))
                {

                    myDic.Add("lat", wgsLat);
                    myDic.Add("lon", wgsLon);
                    return myDic;
                }

                Dictionary<string, double> d = delta(wgsLat, wgsLon);
                myDic.Add("lat", wgsLat + d["lat"]);
                myDic.Add("lon", wgsLon + d["lon"]);
                return myDic;
            }
            ////GCJ-02 to WGS-84
            //public Dictionary<string, double> gcj_decrypt($gcjLat, $gcjLon) {
            //    if (outOfChina(gcjLat, gcjLon))
            //        return array('lat' => $gcjLat, 'lon' => $gcjLon);

            //    $d = delta($gcjLat, $gcjLon);
            //    return array('lat' => $gcjLat - $d['lat'], 'lon' => $gcjLon - $d['lon']);
            //}
            //GCJ-02 to WGS-84 exactly
            public Dictionary<string, double> gcj_decrypt_exact(double gcjLat, double gcjLon)
            {
                double initDelta = 0.01;
                double threshold = 0.000000001;
                double dLat = initDelta;
                double dLon = initDelta;
                double mLat = gcjLat - dLat;
                double mLon = gcjLon - dLon;
                double pLat = gcjLat + dLat;
                double pLon = gcjLon + dLon;
                double wgsLat = 0;
                double wgsLon = 0;
                double i = 0;
                while (true)
                {
                    wgsLat = (mLat + pLat) / 2;
                    wgsLon = (mLon + pLon) / 2;
                    Dictionary<string, double> tmp = gcj_encrypt(wgsLat, wgsLon);
                    dLat = tmp["lat"] - gcjLat;
                    dLon = tmp["lon"] - gcjLon;
                    if ((Math.Abs(dLat) < threshold) && (Math.Abs(dLon) < threshold))
                        break;

                    if (dLat > 0) pLat = wgsLat; else mLat = wgsLat;
                    if (dLon > 0) pLon = wgsLon; else mLon = wgsLon;

                    if (++i > 10000) break;
                }
                //console.log(i);
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                myDic.Add("lat", wgsLat);
                myDic.Add("lon", wgsLon);
                return myDic;

            }
            //GCJ-02 to BD-09
            public Dictionary<string, double> bd_encrypt(double gcjLat, double gcjLon)
            {
                double x = (double)gcjLon;
                double y = (double)gcjLat;
                double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * this.x_pi);
                double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * this.x_pi);
                double bdLon = z * Math.Cos(theta) + 0.0065;
                double bdLat = z * Math.Sin(theta) + 0.006;
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                myDic.Add("lat", bdLat);
                myDic.Add("lon", bdLon);
                return myDic;
            }
            //BD-09 to GCJ-02
            public Dictionary<string, double> bd_decrypt(double bdLat, double bdLon)
            {
                double x = bdLon - 0.0065;
                double y = bdLat - 0.006;
                double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);
                double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);
                double gcjLon = z * Math.Cos(theta);
                double gcjLat = z * Math.Sin(theta);
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                myDic.Add("lat", gcjLat);
                myDic.Add("lon", gcjLon);
                return myDic;
            }

            //WGS-84 to Web mercator
            //$mercatorLat -> y $mercatorLon -> x
            public Dictionary<string, double> mercator_encrypt(double wgsLat, double wgsLon)
            {
                double x = wgsLon * 20037508.34 / 180.0;
                double y = Math.Log(Math.Tan((90.0 + wgsLat) * PI / 360.0)) / (PI / 180.0);
                y = y * 20037508.34 / 180.0;
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                myDic.Add("lat", y);
                myDic.Add("lon", x);
                return myDic;
                /*
                if ((abs($wgsLon) > 180 || abs($wgsLat) > 90))
                    return NULL;
                $x = 6378137.0 * $wgsLon * 0.017453292519943295;
                $a = $wgsLat * 0.017453292519943295;
                $y = 3189068.5 * log((1.0 + sin($a)) / (1.0 - sin($a)));
                return array('lat' => $y, 'lon' => $x);
                //*/
            }
            // Web mercator to WGS-84
            // $mercatorLat -> y $mercatorLon -> x
            public Dictionary<string, double> mercator_decrypt(double mercatorLat, double mercatorLon)
            {
                double x = mercatorLon / 20037508.34 * 180.0;
                double y = mercatorLat / 20037508.34 * 180.0;
                y = 180 / PI * (2 * Math.Atan(Math.Exp(y * PI / 180.0)) - PI / 2);
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                myDic.Add("lat", y);
                myDic.Add("lon", x);
                return myDic;
                /*
                if (abs($mercatorLon) < 180 && abs($mercatorLat) < 90)
                    return NULL;
                if ((abs($mercatorLon) > 20037508.3427892) || (abs($mercatorLat) > 20037508.3427892))
                    return NULL;
                $a = $mercatorLon / 6378137.0 * 57.295779513082323;
                $x = $a - (floor((($a + 180.0) / 360.0)) * 360.0);
                $y = (1.5707963267948966 - (2.0 * atan(exp((-1.0 * $mercatorLat) / 6378137.0)))) * 57.295779513082323;
                return array('lat' => $y, 'lon' => $x);
                //*/
            }
            // two point's distance
            public double distance(double latA, double lonA, double latB, double lonB)
            {
                double earthR = 6371000.0;
                double x = Math.Cos(latA * PI / 180.0) * Math.Cos(latB * PI / 180.0) * Math.Cos((lonA - lonB) * PI / 180);
                double y = Math.Sin(latA * PI / 180.0) * Math.Sin(latB * PI / 180.0);
                double s = x + y;
                if (s > 1) s = 1;
                if (s < -1) s = -1;
                double alpha = Math.Acos(s);
                double distance = alpha * earthR;
                return distance;
            }

            private Dictionary<string, double> delta(double lat, double lon)
            {
                // Krasovsky 1940
                //
                // a = 6378245.0, 1/f = 298.3
                // b = a * (1 - f)
                // ee = (a^2 - b^2) / a^2;
                double a = 6378245.0;//  a: 卫星椭球坐标投影到平面地图坐标系的投影因子。
                double ee = 0.00669342162296594323;//  ee: 椭球的偏心率。
                double dLat = transformLat(lon - 105.0, lat - 35.0);
                double dLon = transformLon(lon - 105.0, lat - 35.0);
                double radLat = lat / 180.0 * PI;
                double magic = (double)Math.Sin(radLat);
                magic = 1 - ee * magic * magic;
                double sqrtMagic = Math.Sqrt(magic);
                dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * PI);
                dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * PI);
                Dictionary<string, double> myDic = new Dictionary<string, double>();
                myDic.Add("lat", dLat);
                myDic.Add("lon", dLon);
                return myDic;
            }

            private bool outOfChina(double lat, double lon)
            {
                if (lon < 72.004 || lon > 137.8347)
                    return true;
                if (lat < 0.8293 || lat > 55.8271)
                    return true;
                return false;
            }

            private double transformLat(double x, double y)
            {
                double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y + 0.2 * Math.Sqrt(Math.Abs(x));
                ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;
                ret += (20.0 * Math.Sin(y * PI) + 40.0 * Math.Sin(y / 3.0 * PI)) * 2.0 / 3.0;
                ret += (160.0 * Math.Sin(y / 12.0 * PI) + 320 * Math.Sin(y * PI / 30.0)) * 2.0 / 3.0;
                return ret;
            }

            private double transformLon(double x, double y)
            {
                double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1 * Math.Sqrt(Math.Sqrt(x));
                ret += (20.0 * Math.Sin(6.0 * x * PI) + 20.0 * Math.Sin(2.0 * x * PI)) * 2.0 / 3.0;
                ret += (20.0 * Math.Sin(x * PI) + 40.0 * Math.Sin(x / 3.0 * PI)) * 2.0 / 3.0;
                ret += (150.0 * Math.Sin(x / 12.0 * PI) + 300.0 * Math.Sin(x / 30.0 * PI)) * 2.0 / 3.0;
                return ret;
            }
        }
        public Dictionary<string, double> wgs84Tosh(double lat, double lon)
        {
            double PI = 3.141592653589793;

            double r2d = 57.2957795131;

            double tolat = (31 + (14.0 + 7.55996 / 60.0) / 60.0) / r2d;

            double tolon = (121.0 + (28.0 + 1.80651 / 60.0) / 60) / r2d;

            double rearth = 6371006.84;

            double hor, frlat, frlon, gcdist, clatf, clatt, slatf, slatt, gcbrg;

            double dlon, cdlon, sdlon, sdist, cdist, sbrg, cbrg, temp;

            double intlat, intlon;

            intlat = lat;

            intlon = lon;

            frlat = lat / r2d;

            frlon = lon / r2d;

            clatt = Math.Cos(frlat);

            clatf = Math.Cos(tolat);

            slatt = Math.Sin(frlat);

            slatf = Math.Sin(tolat);

            dlon = frlon - tolon;

            cdlon = Math.Cos(dlon);

            sdlon = Math.Sin(dlon);

            cdist = slatf * slatt + clatf * clatt * cdlon;

            temp = (clatt * sdlon) * (clatt * sdlon) + (clatf * slatt - slatf * clatt * cdlon) * (clatf * slatt - slatf * clatt * cdlon);

            sdist = Math.Sqrt(Math.Abs(temp));

            if ((Math.Abs(sdist) > 1e-7) || (Math.Abs(cdist) > 1e-7))

                gcdist = Math.Atan2(sdist, cdist);

            else

                gcdist = 0;

            sbrg = sdlon * clatt;

            cbrg = (clatf * slatt - slatf * clatt * cdlon);

            if ((Math.Abs(sbrg) > 1e-7) || (Math.Abs(cbrg) > 1e-7))
            {

                temp = Math.Atan2(sbrg, cbrg);

                while (temp < 0)
                {

                    temp = temp + 2 * PI;

                    gcbrg = temp;

                }

            }
            else

                gcbrg = 0;

            hor = gcdist * rearth;

            double xx = hor * Math.Sin(temp);

            double yy = hor * Math.Cos(temp);
            Dictionary<string, double> myDic = new Dictionary<string, double>();
            myDic.Add("lat", xx);
            myDic.Add("lon", yy);
            return myDic;
        }

        private void BD2SHSelctFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string FileName = this.openFileDialog1.FileName;
                // 你的 处理文件路径代码    
                CSVUtil csvtest = new CSVUtil();
                data = CSVUtil.ReadCSV(FileName);
                this.cb_FiledLng.Items.Clear();
                this.cb_FieldLat.Items.Clear();
                for (int i = 0; i < data[0].Length; i++)
                {
                    this.cb_FiledLng.Items.Add(data[0][i]);
                    this.cb_FieldLat.Items.Add(data[0][i]);
                }
            }
        }

        private void bt_DoTransfer_Click(object sender, EventArgs e)
        {
            String saveFileName = getSaveFilePath();
            if (data.Count == 0)
            {
                MessageBox.Show("请先选择要转换的数据");
            }
            else if (this.cb_FieldLat.SelectedIndex == -1 || this.cb_FiledLng.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择经纬度字段");
            }
            else if (saveFileName.Equals(""))
            {
                MessageBox.Show("请先选择结果保存路径");
            }
            else
            {
                StreamWriter fileWriter = new StreamWriter(saveFileName, false, UnicodeEncoding.GetEncoding("UTF-8"));
               
                String beTransferCoor=this.cb_BeTransCoor.SelectedItem.ToString();
                String toTransferCoor=this.cb_ToTransCoor.SelectedItem.ToString();
               
                GPS gpsc = new GPS();

                if (beTransferCoor.Equals("BD-09"))                                                                 //百度坐标 转换为其他
                {
                    if (toTransferCoor.Equals("Shanghai"))
                    {
                        fileWriter.WriteLine("X,Y");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> bdj = gpsc.bd_decrypt(lat, lng);                               //百度坐标 转为 GCJ-02
                            Dictionary<string, double> wgs84 = gpsc.gcj_decrypt_exact(bdj["lat"], bdj["lon"]);        //GCJ-02 转为 WGS-84坐标
                            Dictionary<string, double> result = wgs84Tosh(wgs84["lat"], wgs84["lon"]);                //WGS84 转为 上海地方坐标系        
                            fileWriter.WriteLine(result["lat"].ToString() + "," + result["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                    else if (toTransferCoor.Equals("GCJ-02"))
                    {
                        fileWriter.WriteLine("Lat,Lng");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> bdj = gpsc.bd_decrypt(lat, lng);                               //百度坐标 转为 GCJ-02     
                            fileWriter.WriteLine(bdj["lat"].ToString() + "," + bdj["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                    else
                    {
                        MessageBox.Show("无相应转换方式");
                    }

                }
                else if (beTransferCoor.Equals("GCJ-02"))                                                          /*********************  GCJ-02转换其他**/
                {
                    if (toTransferCoor.Equals("BD-09"))
                    {
                        fileWriter.WriteLine("Lat,Lng");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> bd = gpsc.bd_encrypt(lat, lng);                               //GCJ-02 转为 百度坐标
                            fileWriter.WriteLine(bd["lat"].ToString() + "," + bd["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                    else if (toTransferCoor.Equals("WGS-84"))
                    {
                        fileWriter.WriteLine("Lat,Lng");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> wgs84 = gpsc.gcj_decrypt_exact(lat, lng);                               //GCJ-02 转为 WGS-84
                            fileWriter.WriteLine(wgs84["lat"].ToString() + "," + wgs84["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                    else if (toTransferCoor.Equals("Shanghai"))                                                                
                    {
                        fileWriter.WriteLine("X,Y");
                        for (int i = 1; i < data.Count; i++)
                        {
                            String[] a = data[i];
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> wgs84 = gpsc.gcj_decrypt_exact(lat, lng);                               //GCJ-02 转为 WGS-84
                            Dictionary<string, double> result = wgs84Tosh(wgs84["lat"], wgs84["lon"]);                         //WGS-84 转为 上海地方坐标系        
                            fileWriter.WriteLine(result["lat"].ToString() + "," + result["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }

                }
                else if (beTransferCoor.Equals("WGS-84"))                                                                  //****WGS-84 to 其他
                {
                    if (toTransferCoor.Equals("GCJ-02"))
                    {
                        fileWriter.WriteLine("Lat,Lng");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> gcj = gpsc.gcj_encrypt(lat, lng);                                 //WGS-84 to GCJ-02
                            fileWriter.WriteLine(gcj["lat"].ToString() + "," + gcj["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                    else if (toTransferCoor.Equals("Web mercator"))
                    {
                        fileWriter.WriteLine("Lat,Lng");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> webm = gpsc.mercator_encrypt(lat, lng);                                 //WGS-84 to Web mercator
                            fileWriter.WriteLine(webm["lat"].ToString() + "," + webm["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                    else if (toTransferCoor.Equals("Shanghai"))
                    {
                        fileWriter.WriteLine("X,Y");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> result = wgs84Tosh(lat,lng);                                           //WGS-84 转为 上海地方坐标系        
                            fileWriter.WriteLine(result["lat"].ToString() + "," + result["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                }
                else if (beTransferCoor.Equals("Web mercator"))
                {
                    if (toTransferCoor.Equals("WGS-84"))
                    {
                        fileWriter.WriteLine("Lat,Lng");
                        for (int i = 1; i < data.Count; i++)
                        {
                            double lat = Convert.ToDouble(data[i][this.cb_FieldLat.SelectedIndex]);
                            double lng = Convert.ToDouble(data[i][this.cb_FiledLng.SelectedIndex]);
                            Dictionary<string, double> wgs84 = gpsc.mercator_decrypt(lat, lng);                                 //Web mercator to wgs-84
                            fileWriter.WriteLine(wgs84["lat"].ToString() + "," + wgs84["lon"].ToString());
                        }
                        fileWriter.Flush();
                        fileWriter.Close();
                        MessageBox.Show("转换成功");
                    }
                }
                else
                {
                    MessageBox.Show("无相应转换方式");
                }
                
            }
        }

        private void bt_SaveFileClick(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lb_SaveFilePath.Text = this.saveFileDialog1.FileName;
            }
        }
        private String getSaveFilePath()
        {
            return this.saveFileDialog1.FileName;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            String beTranCoorName = this.cb_BeTransCoor.SelectedItem.ToString();
            if (beTranCoorName.Equals("BD-09"))
            {
                this.cb_ToTransCoor.Items.Clear();
                this.cb_ToTransCoor.Items.Add("GCJ-02");
                this.cb_ToTransCoor.Items.Add("Shanghai");
            }
            else if (beTranCoorName.Equals("GCJ-02"))
            {
                this.cb_ToTransCoor.Items.Clear();
                this.cb_ToTransCoor.Items.Add("BD-09");
                this.cb_ToTransCoor.Items.Add("WGS-84");
                this.cb_ToTransCoor.Items.Add("Shanghai");
            }
            else if (beTranCoorName.Equals("WGS-84"))
            {
                this.cb_ToTransCoor.Items.Clear();
                this.cb_ToTransCoor.Items.Add("GCJ-02");
                this.cb_ToTransCoor.Items.Add("Web mercator");
                this.cb_ToTransCoor.Items.Add("Shanghai");
            }
            else if (beTranCoorName.Equals("Web mercator"))
            {
                this.cb_ToTransCoor.Items.Clear();
                this.cb_ToTransCoor.Items.Add("WGS-84");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = this.cb_FiledLng.SelectedIndex;
        }

    }
}