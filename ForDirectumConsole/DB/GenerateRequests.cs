namespace ForDirectumConsole.Data
{
    internal class GenerateRequests
    {
        public string AddMeet(string title, DateTime start, DateTime end, int alert)
        {
            return $"INSERT INTO meets (Title, StartMeet, EndMeet, Alert) VALUES (\'{title}\', \'{start}\', \'{end}\', \'{alert}\')";
        }
        public string DeleteMeet(int index)
        {
            return $"DELETE FROM meets WHERE id={index}";
        }
        public string EditMeetForTitle(string title, int index)
        {
            return $"UPDATE meets SET Title = \'{title}\' WHERE id = {index}";
        }
        public string EditMeetForStart(DateTime start, int index)
        {
            return $"UPDATE meets SET StartMeet = \'{start}\' WHERE id = {index}";
        }
        public string EditMeetForEnd(DateTime end, int index)
        {
            return $"UPDATE meets SET EndMeet = \'{end}\' WHERE id = {index}";
        }
        public string EditMeetForAlert(int alert, int index)
        {
            return $"UPDATE meets SET Alert = \'{alert}\' WHERE id = {index}";
        }
        public string GetMeets()
        {
            return $"SELECT * FROM meets";
        }
        public string CheckMeet(DateTime start, DateTime end)
        {;
            return $"SELECT * FROM meets WHERE(startmeet BETWEEN \'{start}\' and \'{end}\') OR (endmeet BETWEEN \'{start}\' and \'{end}\')";
        }

        public string GetMeetsForUnloading(string date)
        {
            return $"SELECT * FROM meets WHERE startmeet >= \'{date} 00:00:00\' and startmeet <= \'{date} 23:59:59\'";
        }
    }
}
