using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JulaneSuzanneDB
{
    public class KeywordsClass/* : IEnumerable<String>*/
    {
        String keywordName;
        List<String> keywords = new List<String>();
        bool keywordsChanged = false;

        public KeywordsClass (String progNameReceived)
        {
            keywordName = ".keyword_" + progNameReceived + ".txt";
        }

        public IEnumerator<String> GetEnumerator()
        {
            foreach (String val in keywords)
            {
                // Yield each value. 
                yield return val;
            }
        }

        public List<String> copy()
        {
            List<String> newList = new List<String>();
            foreach (String kw in keywords)
            {
                newList.Add(kw);
            }
            return newList;
        }

        public void paste(List<String> kwds)
        {
            keywords.Clear();
            foreach (String kw in kwds)
            {
                keywords.Add(kw);
            }
        }

        public void add(String val)
        {
            if (!keywords.Contains(val))
            {
                keywords.Add(val);
                keywords.Sort();
                keywordsChanged = true;
            }
        } // public void add()

        public void set(int index, String val)
        {
            keywords[index] = val;
            keywords.Sort();
            keywordsChanged = true;
        } // public void set()

        public void remove(String val)
        {
            int index;

            index = keywords.IndexOf(val);
            if (index > -1)
            {
                keywords.Remove(val);
                keywordsChanged = true;
            }
        } // public void remove(String val)

        public void clear()
        {
            keywords.Clear();
        }

        public void retrieveFileData()
        {
            String tempS;

            String aa = Directory.GetCurrentDirectory();
            if (File.Exists(keywordName)) {
                // Read Keyword file
                using (StreamReader keywordFile = File.OpenText(keywordName)) {
                    while ((tempS = keywordFile.ReadLine()) != null) {
                        if (!keywords.Contains(tempS))
                        {
                            keywords.Add(tempS);
                        }
                    }
                }
            }
            else {
                // Create Keyword file
                using (StreamWriter keywordFile = File.CreateText(keywordName)) {
                }
            }
            keywords.Sort();
        } // public void retrieveFileData()

        public void save() {
            if (keywordsChanged)
            {
                // Update Keyword file
                using (StreamWriter keywordFile = new StreamWriter(keywordName))
                {
                    foreach (String kw in keywords)
                    {
                        keywordFile.WriteLine(kw);
                    }
                }
            }
        } // public void save()
    }
}
