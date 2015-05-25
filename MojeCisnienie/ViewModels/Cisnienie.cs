using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojeCisnienie.ViewModels
{
    public class Cisnienie
    {
        public int Rozkurczowe { get; set; }
        public int Skurczowe { get; set; }
        public int Tetno { get; set; }
        public DateTime Data { get; set; }
        public string Notatka { get; set; }

        public override string ToString()
        {
            return string.Format("Cisnienie <Rozkurczowe: {0}, Skurczowe: {1}, Tetno {2}, Notatka {3}", Rozkurczowe, Skurczowe, Tetno, Notatka);
        }

        public string statusPomiaru
        {
            get {
                 if (Rozkurczowe <= 90 && Skurczowe <= 60) {
                return "Niskie";
                }
                else if (Rozkurczowe <= 120 && Skurczowe <= 80) {
                    return "Optymalne";
                }
                else if (Rozkurczowe <= 140 && Skurczowe <= 90)
                {
                    return "Podwyzszone";
                }
                else if (Rozkurczowe <= 190 && Skurczowe <= 100) {
                    return "Wysokie";
                }
                return "Brak informacji";
            }
        }
    }

   
}
