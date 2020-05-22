using System;

namespace Refactoring
{

    public class Order{
        public double getAmount(){
            return 10.0;
        }
    }
    public class Extract_Method_SOURCE
    {
        string name = "";

        public void PrintOwing()
        {
            double outstanding = 0.0;

            printBanner();
            
            // 取得數量
            Order o = new Order();
            outstanding = o.getAmount();
            
            Console.WriteLine($@"name:{name}");
            Console.WriteLine($@"name:{outstanding}");
        }

        private void printBanner(){
            Console.WriteLine("Print Banner");
        }
    }

    /// <summary>
    /// 有區域變數
    /// </summary>
    public class Extract_Method_DEMO1
    {
        string name = "";

        public void PrintOwing()
        {
            double outstanding = 0.0;

            printBanner();

            // 取得數量
            Order o = new Order();
            outstanding = o.getAmount();
            
            printDetail(outstanding);
        }

        private void printBanner(){
            Console.WriteLine("Print Banner");
        }

        private void printDetail(double outstanding){
            
            Console.WriteLine($@"name:{name}");
            Console.WriteLine($@"amount:{outstanding}");
        }
    }

    /// <summary>
    /// 有區域變數再賦予值
    /// </summary>
    public class Extract_Method_DEM2
    {
        string name = "";

        public void PrintOwing()
        {
            double outstanding = 0.0;

            printBanner();

            outstanding = getoutstanding();
            
            printDetail(outstanding);
        }

        private void printBanner(){
            Console.WriteLine("Print Banner");
        }

        private void printDetail(double outstanding){
            
            Console.WriteLine($@"name:{name}");
            Console.WriteLine($@"amount:{outstanding}");
        }

        private double getoutstanding(){
            double result = 0.0;
            // 取得數量
            Order o = new Order();
            result = o.getAmount();
           return result;
        }
    }
}