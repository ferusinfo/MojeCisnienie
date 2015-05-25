using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;

namespace MojeCisnienie.ViewModels
{
    public class PomiaryList
    {
        public const string DataFile = "cisnienie.json";
        public List<Cisnienie> ListaPomiarow { get; set; }
        public List<Cisnienie> HistoriaPomiarow { get; set; }
        public double SredniaGora { get; set; }
        public double SredniDol { get; set; }

        public double MinimumGora { get; set; }
        public double MinimumDol { get; set; }

        public double MaksimumGora { get; set; }
        public double MaksimumDol { get; set; }

        public Cisnienie OstatniPomiar { get; set; }
        public Boolean isDataLoaded { get; set; }

        public Boolean HaveEntries()
        {
            return ListaPomiarow.Count > 0 ? true : false;
        }

        public PomiaryList()
        {
            ListaPomiarow = new List<Cisnienie>();
            HistoriaPomiarow = new List<Cisnienie>();

            this.isDataLoaded = false;
        }


        public void removeItem(int id)
        {
            HistoriaPomiarow.RemoveAt(id);
            ListaPomiarow = HistoriaPomiarow;
            ListaPomiarow.Reverse();

            saveListaPomiarow();
        }
  

        private void saveListaPomiarow()
        {
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(DataFile, FileMode.Create, FileAccess.Write, myIsolatedStorage)))
            {
                Debug.WriteLine("writing to file {0}",DataFile);
                foreach (Cisnienie pomiar in ListaPomiarow)
                {
                    string json = JsonConvert.SerializeObject(pomiar);
                    writeFile.WriteLine(json);
                   
                }
                writeFile.Close();
            }
        }


        public void dodajPomiar(int rozkurczowe, int skurczowe, int tetno, string notatka)
        {
            Debug.WriteLine("Dodawanie cisnienia..");
                Cisnienie _cisnienie = new Cisnienie();
                _cisnienie.Rozkurczowe = rozkurczowe;
                _cisnienie.Skurczowe = skurczowe;
                _cisnienie.Tetno = tetno;
                _cisnienie.Notatka = notatka;
                _cisnienie.Data = DateTime.Now;

                string json = JsonConvert.SerializeObject(_cisnienie);

                Debug.WriteLine(json);
                
                IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

                using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(DataFile, FileMode.Append, FileAccess.Write, myIsolatedStorage)))
                {
                    Debug.WriteLine("Adding new record, writing to file {0}", DataFile);
                    writeFile.WriteLine(json);
                    writeFile.Close();
                }
        }

        public void loadData()
        {
            ListaPomiarow = new List<Cisnienie>();
            HistoriaPomiarow = new List<Cisnienie>();

            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(DataFile, FileMode.OpenOrCreate, FileAccess.Read);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                        Cisnienie pomiar = JsonConvert.DeserializeObject<Cisnienie>(line);
                        Debug.WriteLine(pomiar.ToString());
                        ListaPomiarow.Add(pomiar);
                }
            }

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            Debug.WriteLine("Jestem tutaj, przed sprawdzeniem.");
            if (ListaPomiarow.Count > 0)
            {
                Debug.WriteLine("Wszedlem w tego ifa");
                OstatniPomiar = ListaPomiarow.Last();
                SredniaGora = ListaPomiarow.Average(t => t.Rozkurczowe);
                SredniDol = ListaPomiarow.Average(t => t.Skurczowe);

                MinimumGora = ListaPomiarow.Min(t => t.Rozkurczowe);
                MinimumDol = ListaPomiarow.Min(t => t.Skurczowe);

                MaksimumGora = ListaPomiarow.Max(t => t.Rozkurczowe);
                MaksimumDol = ListaPomiarow.Max(t => t.Skurczowe);

                Debug.WriteLine("Przeszedlem dalej..");
               // Setup a Live-Tile that can be pinned.
                string content = string.Format("{0}/{1}", OstatniPomiar.Rozkurczowe,OstatniPomiar.Skurczowe);
                IconicTileData iconicTileData = new IconicTileData();
                iconicTileData.Title = content;
                iconicTileData.Count = 0;

                ShellTile primaryTile = ShellTile.ActiveTiles.First();
                primaryTile.Update(iconicTileData);
                Debug.WriteLine("Tutaj tez jestem.");
            }
            else
            {
                Debug.WriteLine("Pusta lista.");
                SredniaGora = 0;
                SredniDol = 0;
                MinimumGora = 0;
                MinimumDol = 0;
                MaksimumGora = 0;
                MaksimumDol = 0;
                OstatniPomiar = new Cisnienie();
                OstatniPomiar.Rozkurczowe = 0;
                OstatniPomiar.Skurczowe = 0;
                OstatniPomiar.Tetno = 0;
            }

            HistoriaPomiarow = ListaPomiarow;
            HistoriaPomiarow.Reverse();
           
            this.isDataLoaded = true;
        }
    }
}
