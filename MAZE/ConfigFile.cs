using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MAZE
{
    public static class ConfigFile
    {

        public static string Attrib_Type = "Type";
        public static string Attrib_Name = "Name";

        public static string TypeConfig_Excel = "Excel File";
        public static string TypeConfig_ACCDB = "ACC DB";

        public static string AttribExcel_FilePath = "FilePath";
        public static string AttribExcel_Host = "Host";
        public static string AttribExcel_User = "User";
        public static string AttribExcel_Password = "Password";
        public static string AttribExcel_Destination = "Destination";
        public static string AttribExcel_Period = "Period";
        public static string AttribExcel_NamePrefix = "NamePrefix";
        public static string AttribExcel_ModifiedOnly = "ModifiedOnly";
        //public static string AttribExcel_ExcelName = "ExcelName";
        //public static string AttribExcel_TabName = "TabName";
        //public static string AttribExcel_TagName = "TagName";

        public static string AttribAccdb_FilePath = "FilePath";
        public static string AttribAccdb_Host = "Host";
        public static string AttribAccdb_User = "User";
        public static string AttribAccdb_Password = "Password";
        public static string AttribAccdb_Destination = "Destination";
        public static string AttribAccdb_Period = "Period";
        public static string AttribAccdb_NamePrefix = "NamePrefix";
        public static string AttribAccdb_ModifiedOnly = "ModifiedOnly";


        public static string InstalPath = AppDomain.CurrentDomain.BaseDirectory;


        ////Get the value of a atribute from one configuration
        //public static string load_atribute(string config_name, string atribute_name)
        //{
        //    string ConfigFile = InstalPath + "\\Configuration.xml";
        //    if (File.Exists(ConfigFile))
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(ConfigFile);


        //        if (doc.SelectSingleNode("/"+config_name).Attributes[atribute_name] != null)
        //        {
        //            return doc.SelectSingleNode("/" + config_name).Attributes[atribute_name].Value;
        //        }
        //        else
        //        {
        //            LogFile.write_LogFile("Atribute not found for config: " + config_name);
        //            return "";
        //        }
        //    }
        //    else
        //    {
        //        //Creates new file with null text
        //        File.WriteAllText(ConfigFile, "<AllConfig></AllConfig>");
        //        LogFile.write_LogFile("Config file not found for config: " + config_name + ". New Blank Configfile created");
        //        return "";
        //    }
            
        //}

        public static void CreateNewConfig(string m_Configname)
        {
            string ConfigFile = InstalPath + "\\Configuration.xml";
            if (!File.Exists(ConfigFile))
            {
                File.WriteAllText(ConfigFile, "<AllConfig></AllConfig>");
                LogFile.write_LogFile("Config file not found. New file created and config " + m_Configname + "was added");
            }

                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);
                XmlNode ConfigNode = doc.CreateElement("Config");
                XmlAttribute ConfigName = doc.CreateAttribute(Attrib_Name);
                doc.DocumentElement.AppendChild(ConfigNode);
                ConfigNode.Attributes.Append(ConfigName);
                ConfigName.Value = m_Configname;
                doc.Save(ConfigFile);

        }

        public static void DeleteConfig(string m_Configname)
        {
            string ConfigFile = InstalPath + "\\Configuration.xml";
            if (File.Exists(ConfigFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);


                foreach (XmlNode itemNode in doc.SelectNodes("/AllConfig/Config"))
                {
                    if (itemNode.Attributes[Attrib_Name] != null && itemNode.Attributes[Attrib_Name].Value == m_Configname)
                    {
                        doc.DocumentElement.RemoveChild(itemNode);
                        doc.Save(ConfigFile);
                        return;
                    }
                }
            }
            else
            {
                LogFile.write_LogFile("Config file not found during deletion of config: " + m_Configname + ".");
            }

        }

        /// <summary>
        /// Returns a List fo Strings with all Config Names from the config File.
        /// </summary>
        /// <returns></returns>
        public static List<string> ConfigNames()
        {
            List<string> configNames = new List<string>();
            string ConfigFile = InstalPath + "\\Configuration.xml";
            if (File.Exists(ConfigFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);

                foreach (XmlNode itemNode in doc.SelectNodes("/AllConfig/Config"))
                {
                    if (itemNode.Attributes[Attrib_Name] != null)
                        configNames.Add(itemNode.Attributes[Attrib_Name].Value);
                }
      
            }
            else
            {
                LogFile.write_LogFile("Config file not found during load of config names. New blank file created.");
                File.WriteAllText(ConfigFile, "<AllConfig></AllConfig>");
            }
            return configNames;
        }

        public static void RenameConfig(string m_Configname, string m_NewConfigname)
        {
            string ConfigFile = InstalPath + "\\Configuration.xml";
            if (File.Exists(ConfigFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);

                foreach (XmlNode itemNode in doc.SelectNodes("/AllConfig/Config"))
                {
                    if (itemNode.Attributes[Attrib_Name] != null && itemNode.Attributes[Attrib_Name].Value == m_Configname)
                    {
                        itemNode.Attributes[Attrib_Name].Value = m_NewConfigname;
                        doc.Save(ConfigFile);
                        return;
                    }
                }
            }
            else
            {
                LogFile.write_LogFile("Config file not found during rename of config: "+ m_Configname);
            }

        }


        //Writes a value to a atribute of a configuration
        public static bool write_attribute(string config_name, string atribute_name, string value)
        {
            string ConfigFile = InstalPath + "\\Configuration.xml";
            if (File.Exists(ConfigFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);

                foreach (XmlNode itemNode in doc.SelectNodes("/AllConfig/Config"))
                {
                    if (itemNode.Attributes[Attrib_Name] != null && itemNode.Attributes[Attrib_Name].Value == config_name)
                    {
                        if (itemNode.Attributes[atribute_name] != null)
                        {
                            itemNode.Attributes[atribute_name].Value = value;
                        }
                        else
                        {
                            XmlAttribute ConfigName = doc.CreateAttribute(atribute_name);
                            ConfigName.Value = value;
                            itemNode.Attributes.Append(ConfigName);
                        }
                        doc.Save(ConfigFile);
                        return true;
                    }
                }
                LogFile.write_LogFile("Config name \"" + config_name + "\" was not found in config file during attributes update.");
                return false;
            }
            LogFile.write_LogFile("Config file not found during write of attribute \""+atribute_name+" for config \""+ config_name+"\".");
            return false;
        }

        public static string read_attribute(string config_name, string atribute_name)
        {
            string ConfigFile = InstalPath + "\\Configuration.xml";
            if (File.Exists(ConfigFile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigFile);

                foreach (XmlNode itemNode in doc.SelectNodes("/AllConfig/Config"))
                {
                    if (itemNode.Attributes[Attrib_Name] != null && itemNode.Attributes[Attrib_Name].Value == config_name)
                    {
                        if (itemNode.Attributes[atribute_name] != null)
                        {
                            return itemNode.Attributes[atribute_name].Value;
                        }
                        else
                        {
                            return "";
                        }

                    }
                }
                LogFile.write_LogFile("Config name \"" + config_name + "\" or attribute \""+ atribute_name+"not found during read.");
                return "";
            }
            LogFile.write_LogFile("Config File not found during read of attribute \""+atribute_name+"\" for config \""+config_name+"\".");
            return "";
        }

        /// <summary>
        /// Write a new Whitelist Line to a config
        /// </summary>
        /// <param name="config_name"></param>
        /// <param name="atribute_name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static bool write_whitelist(string config_name, string excel_name, string tab_name, string tag_name=null)
        //{
        //    string ConfigFile = InstalPath + "\\Configuration.xml";
        //    if (File.Exists(ConfigFile))
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(ConfigFile);

        //        foreach (XmlNode itemNode in doc.SelectNodes("/AllConfig/Config"))
        //        {
        //            if (itemNode.Attributes[Attrib_Name] == null || itemNode.Attributes[Attrib_Name].Value != config_name)
        //                continue;

        //            foreach (XmlNode whiteNode in itemNode.SelectNodes("/WhiteList"))
        //            {


        //                if (whiteNode.Attributes[AttribExcel_ExcelName] != null)
        //                {
        //                    whiteNode.Attributes[AttribExcel_ExcelName].Value = excel_name;
        //                }
        //                else
        //                {
        //                    XmlAttribute ConfigName = doc.CreateAttribute(AttribExcel_ExcelName);
        //                    ConfigName.Value = excel_name;
        //                    whiteNode.Attributes.Append(ConfigName);
        //                }
        //                doc.Save(ConfigFile);
        //                return true;
        //            }

        //            LogFile.write_LogFile("Config name \"" + config_name + "\" has no whitelist nodes for writing whitelist.");
        //            return false;
        //        }
        //    }
        //        LogFile.write_LogFile("Config name \"" + config_name + "\" was not found for writing whitelist.");
        //        return false;
           
        //}






    }
}
