using CashApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cash_Register_2
{
    class Program
    {
        static void Main(string[] args)
        {
            CashRegisterView view = new CashRegisterView();
            CashRegisterModel model = new CashRegisterModel();
            CashRegisterController controller = new CashRegisterController(view, model);

            Application.Run(new Form1());
            ;
        }
    }
}
