namespace VS_Monopoly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
            //    Console.SetWindowSize(1, 1);
            //    Console.SetBufferSize(79, 45);
            //    Console.SetWindowSize(79, 45);
            //    Console.Write(Properties.Resources.standard_uk);
            //    Console.WriteLine(Properties.Resources.standard_uk);
            List<Property> propertyData = BoardGenerator.Generate();
            foreach (Property property in propertyData)
            {
                Console.Write(property.id + " ");
                Console.Write(property.name + " ");
                Console.Write(property.ownable + " ");
                Console.WriteLine(property.price);
            }
            BoardGenerator.Display(propertyData);
            //Console.CursorVisible = false;
            //Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);    
            //}

        }
    }
}