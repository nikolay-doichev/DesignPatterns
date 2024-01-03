namespace AsynchronousFactoryPattern
{
    public class Foo
    {

        private Foo()
        {

        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000);
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }
    class Program
    {
        static async void Main(string[] args)
        {
            //var foo = new foo();
            //await foo.initasync();

            Foo x = await Foo.CreateAsync();

        }
    }
}