using MAZE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MAZE
{
    public partial class MainService : ServiceBase
    {

        private List<ExtractorInterface> Extractors = new List<ExtractorInterface>();

        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

 
 
            List <string> configNames = ConfigFile.ConfigNames();


            try
            {

            
                foreach (string configName in configNames)
                {
                    if (ConfigFile.read_attribute(configName,ConfigFile.Attrib_Type)== ConfigFile.TypeConfig_Excel)
                    {
                        LogFile.write_LogFile("Service thread created for config name \"" + configName + "\" of type \""+ ConfigFile.TypeConfig_Excel+"\"");
                        ExtractorExcel excel = new ExtractorExcel(configName);
                        excel.Start();
                        Extractors.Add(excel);
                    }
                    else if (ConfigFile.read_attribute(configName, ConfigFile.Attrib_Type) == ConfigFile.TypeConfig_ACCDB)
                    {
                        LogFile.write_LogFile("Service thread created for config name \"" + configName + "\" of type \"" + ConfigFile.TypeConfig_ACCDB + "\"");
                        ExtractorAccdb accdb = new ExtractorAccdb(configName);
                        accdb.Start();
                        Extractors.Add(accdb);
                    }
                    else if (ConfigFile.read_attribute(configName, ConfigFile.Attrib_Type) == ConfigFile.TypeConfig_PIConfig)
                    {
                        LogFile.write_LogFile("Service thread created for config name \"" + configName + "\" of type \"" + ConfigFile.TypeConfig_PIConfig + "\"");
                        ExtractorPIConfig PIConfig = new ExtractorPIConfig(configName);
                        PIConfig.Start();
                        Extractors.Add(PIConfig);
                    }

                }

            }
            catch (Exception e)
            {

                LogFile.write_LogFile(e);
                
            }

            

        }

        protected override void OnStop()
        {
            try
            {
                LogFile.write_LogFile("Services stoped");
                foreach (ExtractorInterface Extractor in Extractors)
                {
                    Extractor.Stop();
                }
            }
            catch (Exception e)
            {

                LogFile.write_LogFile(e);

            }



        }
    }
}
