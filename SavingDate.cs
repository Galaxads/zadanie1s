using AvaloniaApplication5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication5
{
    internal class SavingDate
    {
        public static List<Клиенты> klient = Helper.user.Клиентыs.ToList();
        public static List<Posehenium> posehs = Helper.user.Posehenia.ToList();
        public static List<VisitTabl> visits = Helper.user.VisitTabls.ToList();
        public static List<ListTag> listtag = Helper.user.ListTags.ToList();
        public static List<Tag> tag = Helper.user.Tags.ToList();
        public static List<Клиенты> _ClientsDisplayed = []; //Отображаемые элементы
        public static List<List<Клиенты>> _ClientsPages = []; //Список списков элементов (страниц)
        public static List<Клиенты> _ClientsSelection = []; //Выборка элементов
                                                            //Для создания и редактирования клиентов
        public static Клиенты _RedClient = null;
    }
}
