namespace App;

public static class Program
{
        public static void Main()
        {
            var first = new Lab3.User("klim", "123", "Клим", "Садов",
                "123456789012", "+79131234567");

            var second = new Lab3.User("anna", "456", "Анна", "Иванова",
                "987654321098", "+79137654321");

            Console.WriteLine(first.GetUserFullName());
            Console.WriteLine(second.GetUserFullName());

            Console.WriteLine(first.TryUpdatePhone("123"));      
            Console.WriteLine(first.TryUpdatePhone("+79991234567")); 
        }
}