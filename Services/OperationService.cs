using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class OperationService
    {
        public static void addOperation(ApplicationDbContext context, int userID, string operationType,double money =-1)
        {
            Operation newOp = new Operation
            {
                operationDate = DateTime.Now,
                userID        = userID,
                operationType = operationType,
                successStatus = true
            };
            context.Operations.Add(newOp);
            context.SaveChanges();
        }
        public static void listOperation(ApplicationDbContext context, int userID)
        {
            List<Operation> operations = context.Operations.Where(o=>o.userID == userID).ToList();
            foreach (Operation operation in operations)
            {
                Console.WriteLine(operation.toString());
            }
        }
    }
}
