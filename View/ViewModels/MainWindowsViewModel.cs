using System;
using System.Collections.Generic;
using System.Text;
using View.ViewModels.Base;


namespace View.ViewModels
{
    internal class MainWindowsViewModel : ViewModel
    {
        private string _Title = "Анализ цен";

        public string Title { get => _Title; set => Set(ref _Title, value); }
    }
}
