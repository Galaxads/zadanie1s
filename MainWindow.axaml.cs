using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaApplication5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;

namespace AvaloniaApplication5
{
    public partial class MainWindow : Window
    {
        private int _SelectedPageIndex; //Выбранный номер страницы
        private int forswitch;
        //public List<Клиенты> _AllClients = Helper.user.Клиентыs.Include(x => x.Visit).ToList(); //.Include(x => x.ClientsTags).
              //  Include(x => x.VisitsLogs).
               //  Include(x => x.ClientsFiles).ToList(); //Список клиентов из БД (также связанными данными заполнены коллекции в объектах: теги, посещения, файлы)

        public MainWindow()
        {
            InitializeComponent();
            Listins(SavingDate.klient);
            FiltrPol.SelectedIndex = 0;
            Filtr.SelectedIndex = 0;
            cbox_display.SelectedIndex = 0;
            
        }

        private void Listins(List<Клиенты> list)
        {
           
            ListPers.ItemsSource = list.Select(x => new
            {

                x.Id,
                x.Name,
                Gender = x.Gender == 1 ? "Мужчина" : "Женщина",
                x.Surname,
                x.MiddleName,
                x.Birthday,
                x.DateRegistr,
                x.NumberPhone,
                x.EmailAdress,
                // v = x.Id-1 <=SavingDate.posehs.Count-1 && x.Id-1 <= SavingDate.visits.Count - 1 ? SavingDate.visits[x.Id-1].DateVisit :  ,
                // dd = x.Id - 1 <= SavingDate.posehs.Count - 1 ? true : false,
                //Visit = dd==true ? SavingDate.visits[v].DateVisit :  ,
                x.ColvoVisit,
                photo= new Bitmap ($"Assets/{x.Photo}"),
                //Tag = SavingDate.tag[x.Id-1].Name,
               
            });
            ColvoText.Text =$"Выведено записей {SavingDate.klient.Count} из {SavingDate.klient.Count}";
        }


        private void Filtrs(List<Клиенты> clients)
        {
           List<Клиенты> dsf=clients;
            ClientsDisplayed(dsf, forswitch);
            int d = Filtr.SelectedIndex;
            if (d == 1)
            {
                dsf = dsf.OrderBy(x => x.MiddleName).ToList();
                Listins(dsf);

            }
            else if (d == 2)
            {
                dsf= dsf.OrderByDescending(x => x.ColvoVisit).ToList();
                Listins(dsf);
            }
         
            int v = FiltrPol.SelectedIndex;
            if (v == 1)
            {
                Listins(dsf.Where(x => x.Gender == 1).ToList()); ColvoText.Text = $"Выведено записей {SavingDate.klient.Where(x => x.Gender == 1).Count()} из {SavingDate.klient.Count}"; 
            }
            else if(v == 2)
            {
                Listins(dsf.Where(x => x.Gender == 2).ToList()); ColvoText.Text = $"Выведено записей {SavingDate.klient.Where(x => x.Gender == 2).Count()} из {SavingDate.klient.Count}";
            }
       
        }
        private void Filtrs1()
        {
            int d = Filtr.SelectedIndex;
            switch (d)
            {
                case 1: Listins(SavingDate.klient.OrderBy(x => x.MiddleName).ToList()); break;
                case 2: Listins(SavingDate.klient.OrderByDescending(x => x.ColvoVisit).ToList()); break;
            }
        }

        private void ComboBox_SizeChanged(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
        {
            Filtrs(SavingDate.klient);
            
           
        }

        private void ComboBox_SizeChanged_2(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
        {
            Filtrs(SavingDate.klient);
          
        }
        private void ClientsDisplayed(List<Клиенты> clients, int forswitch)
        {
           SavingDate._ClientsPages.Clear();
            int displayedClientsCount = 1;
            switch (forswitch)
            {
                case 1:
                    displayedClientsCount = 10;
                    break;
                case 2:
                    displayedClientsCount = 50;
                    break;
                case 3:
                    displayedClientsCount = 200;
                    break;
                default:
                    Listins(clients);
                    return;
            }
            displayedClientsCount = displayedClientsCount > clients.Count ? clients.Count : displayedClientsCount;
            int listCount = (int)Math.Ceiling((double)clients.Count / displayedClientsCount);
            int l = 0; //Счетчик для всех клиентов
            for (int j = 0; j < listCount; j++)
            {
                List<Клиенты> displayedClients = [];
                int testint = (displayedClientsCount > clients.Count - displayedClientsCount * j ? clients.Count - displayedClientsCount * j : displayedClientsCount);
                for (int i = 0; i < testint; i++)
                {
                    displayedClients.Add(clients[l]);
                    l++;
                }
                SavingDate._ClientsPages.Add(displayedClients);
            }
            _SelectedPageIndex = 0;
            Listins(clients.Count > 0 ? SavingDate._ClientsPages[_SelectedPageIndex] : clients);
            PageTextDisplay( displayedClientsCount);
        }

        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            int v=cbox_display.SelectedIndex;
            ClientsDisplayed(SavingDate.klient,v);
        }
        private void PageTextDisplay(int displayedClientsCount) //отображение номера страницы
        {
            int v=displayedClientsCount;
             if (SavingDate._ClientsPages.Count > 0)
             {
               tblock_page.IsVisible = true;
               tblock_pageCount.IsVisible = true;
               tblock_pageCount.Text = $"{_SelectedPageIndex + 1}/{SavingDate._ClientsPages.Count}"; 
                ColvoText.Text = $"Выведено записей {v} из {SavingDate.klient.Count}";
            }
            else
             {
              tblock_page.IsVisible = tblock_pageCount.IsVisible = false;
             }
        }
        private void Stranich(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (cbox_display.SelectedIndex != 0)
            {
                int displayedClientsCount = 0;
                switch (forswitch)
                {
                    case 1:
                        displayedClientsCount = 10;
                        break;
                    case 2:
                        displayedClientsCount = 50;
                        break;
                    case 3:
                        displayedClientsCount = 200;
                        break;
                }
                var btn = (sender as Button)!;
                switch (btn.Name)
                {
                    case "btn_nazad":
                        _SelectedPageIndex--;
                        if (_SelectedPageIndex >= 0)
                        {

                            PageTextDisplay(displayedClientsCount);
                            Listins(SavingDate._ClientsPages[_SelectedPageIndex]);
                        }
                        else
                        {
                            _SelectedPageIndex++;
                        }
                        break;
                    case "btn_next":
                        _SelectedPageIndex++;
                        if (_SelectedPageIndex <SavingDate._ClientsPages.Count)
                        {
                            PageTextDisplay(displayedClientsCount);
                            Listins(SavingDate._ClientsPages[_SelectedPageIndex]);
                        }
                        else
                        {
                            _SelectedPageIndex--;
                        }
                        break;
                }
            }
        }

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            new Redactor().Show();
            Close();
        }

        private void Button_Click_2(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
            var btn = (sender as Button)!;
            switch (btn.Name)
            {
                case "btn_red":
                   SavingDate._RedClient = SavingDate.klient[((int)btn!.Tag!)-1];//((int)btn!.Tag!)
                    break;
            }

             new Redactor().Show(); Close();
           
        }
    }
}