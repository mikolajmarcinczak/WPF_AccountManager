using AccountManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountManager.Models
{
    public class MainWindowProperties : ObservableObject
    {
        private bool _welcomeLabelVis = true;
        private bool _mainDataGridVis = false;
        private bool _addInfoBtnVis = false;
        private bool _addBillBtnVis = false;
        private string _welcomeMessage = "";
        private int _fontSize;
        private FontWeight _fontWeight;

        public MainWindowProperties()
        {
            this._welcomeLabelVis = true;
            this._mainDataGridVis = false;
            this._addInfoBtnVis = false;
            this._addBillBtnVis = false;
            this._welcomeMessage = "";
    }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
        public FontWeight FontWeight
        {
            get => _fontWeight;
            set
            {
                _fontWeight = value;
                OnPropertyChanged(nameof(FontWeight));
            }
        }
        public bool WelcomeLabelVis { 
            get => _welcomeLabelVis;
            set
            {
                _welcomeLabelVis = value;
                OnPropertyChanged(nameof(WelcomeLabelVis));
            }
        }
        public bool MainDataGridVis { 
            get => _mainDataGridVis;
            set
            {
                _mainDataGridVis = value;
                OnPropertyChanged(nameof(MainDataGridVis));
            }
        }
        public bool AddInfoBtnVis { 
            get => _addInfoBtnVis;
            set
            {
                _addInfoBtnVis = value;
                OnPropertyChanged(nameof(AddInfoBtnVis));
            }
        }
        public bool AddBillBtnVis { 
            get => _addBillBtnVis;
            set
            {
                _addBillBtnVis = value;
                OnPropertyChanged(nameof(AddBillBtnVis));
            }
        }
        public string WelcomeMessage { 
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }
    }
}
