// See https://aka.ms/new-console-template for more information
using ForDirectumConsole;
using ForDirectumConsole.Data;

Console.WriteLine("Добро пожаловать в органайзер встреч!");
Console.WriteLine("Ваши встречи:");

var workWithDB = new WorkWithDB();
var genSQLRequests = new GenerateRequests();

workWithDB.OpenConnection();
string sql = genSQLRequests.GetMeets();
List<Meets> response = workWithDB.QuerySelect(sql);
Print.Display(response);
workWithDB.CloseConnection();

int _choice;

do
{
    Console.WriteLine("\nЧто необходимо сделать?\n1-Показать все встречи\n2-Добавить встречу\n3-Изменить встречу\n4-Удалить встречу\n5-Экспортировать в текстовый документ\n6-Выйти");
    _choice = Convert.ToInt32(Console.ReadLine());
    switch (_choice)
    {
        case 1:
            workWithDB.OpenConnection();
            string sqlForView = genSQLRequests.GetMeets();
            List<Meets> responseForView = workWithDB.QuerySelect(sqlForView);
            Print.Display(responseForView);
            workWithDB.CloseConnection();
            break;
        case 2:
            Console.WriteLine("Введите название встречи:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите начало встречи в формате ДД.ММ.ГГГГ ЧЧ:ММ");
            string start = Console.ReadLine();
            Console.WriteLine("Введите конец встречи в формате ДД.ММ.ГГГГ ЧЧ:ММ");
            string end = Console.ReadLine();
            Console.WriteLine("Предупредить за:");
            string alarm = Console.ReadLine();
            if (DateTime.Parse(start) < DateTime.Now)
            {
                Console.WriteLine("Живи настоящим а не прошлым!");
            }
            else
            {            
            string sqlForCheck = genSQLRequests.CheckMeet(DateTime.Parse(start), DateTime.Parse(end));
            workWithDB.OpenConnection();
            int responseForCheck = workWithDB.QuerySelectCheck(sqlForCheck);
            workWithDB.CloseConnection();
                if (responseForCheck > 0)
                {
                    Console.WriteLine("Уже есть встреча на это время!");
                }
                else 
                {
                    string sqlForAdd = genSQLRequests.AddMeet(title, DateTime.Parse(start), DateTime.Parse(end), int.Parse(alarm));
                    workWithDB.OpenConnection();
                    workWithDB.QueryOther(sqlForAdd);
                    workWithDB.CloseConnection();
                }
            }
            Console.WriteLine("У вас есть новая встреча!");
            break;
        case 3:
            Console.WriteLine("Ща как изменим! Какую встречу надо изменить?");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Что необходимо изменить?\n1-Название встречи\n2-Начало встречи\n3-Конец встречи\n4-Предупреждение");
            int choiceForCase3;
            choiceForCase3 = Convert.ToInt32(Console.ReadLine());
            switch (choiceForCase3)
            {
                case 1:
                    Console.WriteLine("Введите новое название встречи!");
                    string newTitle = Console.ReadLine();
                    workWithDB.OpenConnection();                    
                    string sqlForEditTitle = genSQLRequests.EditMeetForTitle(newTitle, id);
                    workWithDB.QueryOther(sqlForEditTitle);
                    workWithDB.CloseConnection();
                    break;
                case 2:
                    Console.WriteLine("Введите новое время начало встречи в формате ДД.ММ.ГГГГ ЧЧ:ММ");
                    DateTime newStart = DateTime.Parse(Console.ReadLine());
                    workWithDB.OpenConnection();
                    string sqlForEditStart = genSQLRequests.EditMeetForStart(newStart, id);
                    workWithDB.QueryOther(sqlForEditStart);
                    workWithDB.CloseConnection();
                    break;
                case 3:
                    Console.WriteLine("Введите новое время конца встречи в формате ДД.ММ.ГГГГ ЧЧ:ММ");
                    DateTime newEnd =  DateTime.Parse(Console.ReadLine());
                    workWithDB.OpenConnection();
                    string sqlForEditEnd = genSQLRequests.EditMeetForEnd(newEnd, id);
                    workWithDB.QueryOther(sqlForEditEnd);
                    workWithDB.CloseConnection();
                    break;
                case 4:
                    Console.WriteLine("Введите новое время предупреждения встречи!");
                    int newAlert = Int32.Parse(Console.ReadLine());
                    workWithDB.OpenConnection();
                    string sqlForEditAlert = genSQLRequests.EditMeetForAlert(newAlert, id);
                    workWithDB.QueryOther(sqlForEditAlert);
                    workWithDB.CloseConnection();
                    break;
            }
            Console.WriteLine("Изменения внесены!");
            break;
        case 4:
            Console.WriteLine("Ща как удалим! Какой номер встречи удалить?");
            string index = Console.ReadLine();
            workWithDB.OpenConnection();
            string sqlForDetete = genSQLRequests.DeleteMeet(Int32.Parse(index));
            workWithDB.QueryOther(sqlForDetete);
            workWithDB.CloseConnection();
            Console.WriteLine("Вы удалили встречу");
            break;
        case 5:
            Console.WriteLine("За какую дату выгрузить? Введите в формате ДД.ММ.ГГГГ");
            string targetDate = Console.ReadLine();
            workWithDB.OpenConnection();
            string sqlForUnloading = genSQLRequests.GetMeetsForUnloading(targetDate);
            List<Meets> responseForCase5 = workWithDB.QuerySelect(sqlForUnloading);
            workWithDB.CloseConnection();
            try
            {
                StreamWriter sw = new StreamWriter("C:\\Test.txt", false);
                foreach (Meets meet in responseForCase5)
                    sw.WriteLine($"{meet.Id}\t{meet.Title}\t{meet.StartMeet}\t{meet.EndMeet}\t{meet.Alert}");
                sw.Close();
                FileInfo f = new FileInfo(@"C:\Test.txt");
                FileStream s = f.Open(FileMode.Append, FileAccess.Read);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            break;
        case 6:
            Console.WriteLine("Good Bye!");
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Не неси чепухи!");
            break;
    }
} while (_choice != 6);
