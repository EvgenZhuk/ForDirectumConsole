namespace ForDirectumConsole.Data
{
    public class Meets
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartMeet { get; set; }
        public DateTime EndMeet { get; set; } 
        public int Alert { get; set; }
        
        public Meets(int id, string title, DateTime start, DateTime end, int alert)
        {
            Id = id;
            Title = title;
            StartMeet = start;  
            EndMeet = end;
            Alert = alert;
        }
        public Meets()
        {
        }

    }    
}
