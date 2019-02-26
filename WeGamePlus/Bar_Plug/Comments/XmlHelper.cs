using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using WeGamePlus.Bar_Plug.Models;

namespace WeGamePlus.Bar_Plug.Comments
{
    public class XmlHelper
    {
        private static void Close_Click(object sender, EventArgs e)
        {
            try
            {
                ((UserForm) ((PictureBox) ((PictureBox) sender).Parent).Parent).Hide();
            }
            catch (Exception)
            {
            }
        }

        private static void FormItem_Click(object sender, EventArgs e)
        {
            string fileName = null;
            try
            {
                UserPicturebox picturebox = (UserPicturebox) sender;
                if (string.IsNullOrEmpty(picturebox.ClickUrl))
                {
                    switch (picturebox.GameType)
                    {
                        case "2":
                            fileName = GetPublicXmlValue("url_dnf", false);
                            goto Label_0096;

                        case "3":
                            fileName = GetPublicXmlValue("url_cf", false);
                            goto Label_0096;

                        case "4":
                            fileName = GetPublicXmlValue("url_lol", false);
                            goto Label_0096;

                        case "5":
                            fileName = GetPublicXmlValue("url_nz", false);
                            goto Label_0096;
                    }
                    fileName = null;
                }
                else
                {
                    fileName = picturebox.ClickUrl;
                }
            Label_0096:
                StatisticsHelp.Statistics(int.Parse(picturebox.GameType), picturebox.Statistics.ToString(), "");
                if (!fileName.Contains("http"))
                {
                    fileName = GetPublicXmlValue("webip", true) + fileName;
                }
                Process.Start(fileName);
            }
            catch (Exception)
            {
                if (fileName != null)
                {
                    Process.Start("iexplore.exe", fileName);
                }
            }
        }

        private static void FormItem_MouseLeaveAndMouseEnter(object sender, EventArgs e)
        {
            try
            {
                PictureBox box1 = (PictureBox) sender;
                Image backgroundImage = box1.BackgroundImage;
                Image image2 = box1.Image;
                box1.Image = backgroundImage;
                box1.BackgroundImage = image2;
            }
            catch
            {
            }
        }

        public static void GetFrom()
        {
            if (GetPublicXmlValue("isShowFormItem", false) == "0")
            {
                IEnumerable<XElement> enumerable = UrlManage.id_dllSettingDoc.Root.Elements("FormItem");
                if (enumerable != null)
                {
                    foreach (XElement element in enumerable)
                    {
                        UserForm item = new UserForm {
                            text = element.Attribute("text")?.Value,
                            type = element.Attribute("type")?.Value,
                            imageType = element.Attribute("imageType")?.Value,
                            ClickUrl = element.Attribute("ClickUrl")?.Value,
                            statistics = (element.Attribute("statistics") == null) ? 0 : int.Parse(element.Attribute("statistics").Value)
                        };
                        item.AccessibleDescription = item.text;
                        item.AutoSize = true;
                        item.Text = "";
                        item.FormBorderStyle = FormBorderStyle.None;
                        item.isShow = element.Attribute("isShow")?.Value;
                        item.ClassName = element.Attribute("ClassName")?.Value;
                        item.WindowName = element.Attribute("WindowName")?.Value;
                        char[] separator = new char[] { ',' };
                        char[] chArray2 = new char[] { ',' };
                        item.location1 = new Point(int.Parse(element.Attribute("location1")?.Value.Split(separator)[0]), int.Parse(element.Attribute("location1")?.Value.Split(chArray2)[1]));
                        char[] chArray3 = new char[] { ',' };
                        char[] chArray4 = new char[] { ',' };
                        item.size1 = new Size(int.Parse(element.Attribute("size1")?.Value.Split(chArray3)[0]), int.Parse(element.Attribute("size1")?.Value.Split(chArray4)[1]));
                        char[] chArray5 = new char[] { ',' };
                        char[] chArray6 = new char[] { ',' };
                        item.location2 = new Point(int.Parse(element.Attribute("location2")?.Value.Split(chArray5)[0]), int.Parse(element.Attribute("location2")?.Value.Split(chArray6)[1]));
                        char[] chArray7 = new char[] { ',' };
                        char[] chArray8 = new char[] { ',' };
                        item.size2 = new Size(int.Parse(element.Attribute("size2")?.Value.Split(chArray7)[0]), int.Parse(element.Attribute("size2")?.Value.Split(chArray8)[1]));
                        char[] chArray9 = new char[] { ',' };
                        char[] chArray10 = new char[] { ',' };
                        item.location3 = new Point(int.Parse(element.Attribute("location3")?.Value.Split(chArray9)[0]), int.Parse(element.Attribute("location3")?.Value.Split(chArray10)[1]));
                        char[] chArray11 = new char[] { ',' };
                        char[] chArray12 = new char[] { ',' };
                        item.size3 = new Size(int.Parse(element.Attribute("size3")?.Value.Split(chArray11)[0]), int.Parse(element.Attribute("size3")?.Value.Split(chArray12)[1]));
                        item.image = UrlManage.SetImage(element.Attribute("image")?.Value, item.imageType);
                        item.hoverimage = UrlManage.SetImage(element.Attribute("hoverimage")?.Value, item.imageType);
                        char[] chArray13 = new char[] { ',' };
                        char[] chArray14 = new char[] { ',' };
                        item.hideLocation1 = new Point(int.Parse(element.Attribute("hideLocation1")?.Value.Split(chArray13)[0]), int.Parse(element.Attribute("hideLocation1")?.Value.Split(chArray14)[1]));
                        char[] chArray15 = new char[] { ',' };
                        char[] chArray16 = new char[] { ',' };
                        item.hideLocation2 = new Point(int.Parse(element.Attribute("hideLocation2")?.Value.Split(chArray15)[0]), int.Parse(element.Attribute("hideLocation2")?.Value.Split(chArray16)[1]));
                        char[] chArray17 = new char[] { ',' };
                        char[] chArray18 = new char[] { ',' };
                        item.hideLocation3 = new Point(int.Parse(element.Attribute("hideLocation3")?.Value.Split(chArray17)[0]), int.Parse(element.Attribute("hideLocation3")?.Value.Split(chArray18)[1]));
                        item.hideColor1 = element.Attribute("hideColor1")?.Value;
                        item.hideColor2 = element.Attribute("hideColor2")?.Value;
                        item.hideColor3 = element.Attribute("hideColor3")?.Value;
                        UserPicturebox picturebox = new UserPicturebox {
                            Statistics = item.statistics,
                            ClickUrl = item.ClickUrl,
                            Dock = DockStyle.Fill,
                            Cursor = Cursors.Hand,
                            GameType = item.type,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Image = item.image,
                            BackgroundImage = item.hoverimage
                        };
                        picturebox.Click += new EventHandler(XmlHelper.FormItem_Click);
                        picturebox.MouseEnter += new EventHandler(XmlHelper.FormItem_MouseLeaveAndMouseEnter);
                        picturebox.MouseLeave += new EventHandler(XmlHelper.FormItem_MouseLeaveAndMouseEnter);
                        string str = element.Attribute("closeimage")?.Value;
                        if (!string.IsNullOrEmpty(str))
                        {
                            PictureBox box = new PictureBox {
                                Cursor = Cursors.Hand,
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            char[] chArray19 = new char[(element.Attribute("closeSize")?.Value == null) ? 0 : 1];
                            chArray19[0] = ',';
                            string[] strArray = element.Attribute("closeSize")?.Value.Split(chArray19);
                            box.Size = new Size(int.Parse(strArray[0].ToString()), int.Parse(strArray[1].ToString()));
                            box.Location = new Point(0, 0);
                            box.Click += new EventHandler(XmlHelper.Close_Click);
                            box.Image = UrlManage.SetImage(str, "png");
                            box.BringToFront();
                            box.Width = box.Size.Width;
                            box.Height = box.Size.Height;
                            picturebox.Controls.Add(box);
                        }
                        item.Controls.Add(picturebox);
                        if ((item.isShow == "0") && (item != null))
                        {
                            UrlManage.__FormsList.Add(item);
                        }
                    }
                }
            }
        }

        public static string GetPublicXmlAttributeValue(string xmlName, string AttributeName, bool isPublic = false)
        {
            try
            {
                XElement root = null;
                if (isPublic)
                {
                    root = UrlManage.dllSettingDoc.Root;
                }
                else
                {
                    root = UrlManage.id_dllSettingDoc.Root;
                }
                return root.Element(xmlName).Attribute(AttributeName)?.Value;
            }
            catch
            {
                return "";
            }
        }

        public static string GetPublicXmlValue(string xmlName, bool isPublic = false)
        {
            try
            {
                XElement root = null;
                if (isPublic)
                {
                    root = UrlManage.dllSettingDoc.Root;
                }
                else
                {
                    root = UrlManage.id_dllSettingDoc.Root;
                }
                return root.Element(xmlName).Value;
            }
            catch
            {
                return "";
            }
        }

        public static bool IsShowThis(string xmlName, bool isPublic = false)
        {
            try
            {
                XElement root = null;
                if (isPublic)
                {
                    root = UrlManage.dllSettingDoc.Root;
                }
                else
                {
                    root = UrlManage.id_dllSettingDoc.Root;
                }
                return (root.Element(xmlName).Value == "0");
            }
            catch
            {
                return false;
            }
        }
    }
}

