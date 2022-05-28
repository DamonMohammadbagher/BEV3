using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BEV
{
    class MitreAttackClass
    {
        public struct MitreAttack_Attack_Items
        {
            public string Attack_technique_ID { set; get; }           
            public string Name { set; get; }
            public string Description { set; get; }
            public string CommandPrompt { set; get; }

        }

        public  List<MitreAttack_Attack_Items> MitreAttackList = new List<MitreAttack_Attack_Items>();

        public  void DumpYamlInfo(string filename_yaml)
        {
            string dump = "";
            using (StreamReader sw = new StreamReader(filename_yaml))
            {
                dump = sw.ReadToEnd();
              
            }
              
            string[] _Dumps = dump.Split('\n');
            int[] allindex = _FindAllIndex("- name: ", dump, 0);
            string _attack_technique = "";
            string _name = "";
            string _Description = "";
            string _CommandPrompt = "";
            Int32 counter = 0;
            bool descriptionFlag = false;
            bool attacktechniqqueFlag = false;

            //for (int i = 0; i < allindex.Length; i++)
            //{

            //}

            foreach (string item in _Dumps.ToArray())
            {
                if (item.Contains("attack_technique:"))
                {
                    _attack_technique = item;
                }
                else if (item.Contains("- name: "))
                {
                    _name = item;
                }
                else if (item.Contains("  description: |") || descriptionFlag == true)
                {
                    descriptionFlag = true;
                    _Description += item + ", \n";
                    if (item.Contains("supported_platforms:")) descriptionFlag = false;
                }
                else if (item.Contains("    command: |") || attacktechniqqueFlag == true)
                {
                    attacktechniqqueFlag = true;
                    _CommandPrompt += item + "\n";


                    if (item.Contains("    cleanup_command: ") || item.Contains("    name: "))
                    {
                        attacktechniqqueFlag = false;
                        MitreAttackList.Add(new MitreAttack_Attack_Items
                        {
                            Attack_technique_ID = _attack_technique,
                            Name = _name,
                            Description = _Description,
                            CommandPrompt = _CommandPrompt
                        });
                        _attack_technique = "";
                        _name = "";
                        _Description = "";
                        _CommandPrompt = "";
                    }
                }
                counter++;
            }

        }

        public static int[] _FindAllIndex(string match, string Source_to_Search, int StartIndex)
        {
            int found = -1;
            int count = 0;

            List<int> founditems = new List<int>();
            Source_to_Search = Source_to_Search.ToUpper();
            match = match.ToUpper();

            do
            {

                found = Source_to_Search.IndexOf(match, StartIndex);
                if (found > -1)
                {
                    StartIndex = found + match.Length;
                    count++;
                    founditems.Add(found);
                }

            } while (found > -1 && StartIndex < Source_to_Search.Length);

            return ((int[])founditems.ToArray());
        }

    }
}
