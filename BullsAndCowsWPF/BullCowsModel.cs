using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BullsAndCowsWPF
{
    public class BullCowsModel:INotifyPropertyChanged
    {
        string enigma;
        string currentVersion;
        public int Step { get; set; }
        public ObservableCollection<Hypo> Hypothesis { get; } = new ObservableCollection<Hypo>();
        public ObservableCollection<string> Vers { get; } = new ObservableCollection<string>();

        public BullCowsModel()
        {
            Init();
        }

        public void Init()
        {
            Random rnd = new Random();
            char[] digs = "0123456789".ToCharArray();

            for (int i = 0; i < 4; i++)
            {
                int j = rnd.Next(i, 10);
                (digs[i], digs[j]) = (digs[j], digs[i]);
            }
            enigma = new string(digs, 0, 4);

        }

        public string CurrentVersion
        {
            get => currentVersion;
            set
            {
                if (value != currentVersion)
                {
                    currentVersion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(currentVersion)));

                    if(currentVersion.Length==4&&
                        currentVersion.Distinct().Count()==4&&
                        currentVersion.All(ch => '0' <= ch && ch <= '9'))
                    {
                        int bulls = 0, cows = 0;

                        for (int i = 0; i < 4; i++)
                            if (currentVersion[i] == enigma[i]) bulls++;
                         else if (enigma.Contains(currentVersion[i])) cows++;

                        Step++;

                        Hypothesis.Add(new Hypo
                        {
                            Step=Step,
                            Num = currentVersion,
                            Bulls=bulls,
                            Cows=cows
                        });
                        while (Hypothesis.Count > 12)
                            Hypothesis.RemoveAt(0);
                    }
                }
            }
        }

        internal void RemovKey(string dig)
        {
            Vers.Remove(dig);
        }

        internal void PressKey(string dig)
        {
            Vers.Add(dig);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
