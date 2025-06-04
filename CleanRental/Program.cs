namespace CleanRental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new CleanRentalContext();
            var logic = new BusinessLogic(context);
            var tui = new Tui(logic);
            tui.Run();
        }
    }
}



