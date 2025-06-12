using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;

namespace EV_Tuner
{
    internal class DataHandler
    {
        public static void Export(Settings settings)
        {
            Console.WriteLine("Exporting Files");

            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonString = JsonSerializer.Serialize(settings,options);

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Application.StartupPath;
            sfd.FileName = "UVFR-EVTUNER_DATAEXPORT.json";
            sfd.Filter = "JSON (*.json)|*.json|Text (*.txt)|*.txt";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, jsonString);
            }
            else
            {
                Console.WriteLine("Didn't Export File");
            }
        }

        public static Settings Import()
        {
            Console.WriteLine("Importing Files");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "JSON (*.json)|*.json";


            if(ofd.ShowDialog() == DialogResult.OK)
            {
                var importJsonString = File.ReadAllText(ofd.FileName);
                Settings importSettings = JsonSerializer.Deserialize<Settings>(importJsonString);
                return importSettings;
            }
            else
            {
                return null;
            }
        }
    }
}
