using DoAnCuoiKy_KTLT.Entiy;

namespace DoAnCuoiKy_KTLT.XyLyLuuTru
{
    public class LuuTruSaleBill
    {
        public static SaleBill[] ReadSaleBill()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "salebill.txt");
            StreamReader file = new StreamReader(filePath);
            int numSales = int.Parse(file.ReadLine());

            SaleBill[] saleList = new SaleBill[numSales];

            for (int i = 0; i < numSales; i++)
            {
                string[] billInfo = file.ReadLine().Split(';');

                saleList[i].Id = int.Parse(billInfo[0]);
                saleList[i].CreatedDate = DateOnly.Parse(billInfo[1]);

                string detailIDsString = billInfo[2].Trim('[', ']');
                if (string.IsNullOrWhiteSpace(detailIDsString))
                {
                    // Nếu rỗng, gán một mảng rỗng cho detailBillsID
                    saleList[i].detailBillsID = new int[0];
                }
                else
                {
                    string[] detailIDs = detailIDsString.Split(',');
                    saleList[i].detailBillsID = Array.ConvertAll(detailIDs, int.Parse);
                }
            }

            file.Close();

            return saleList;
        }

    
        public static SaleBill? SearchSaleBillById(int id)
        {
            SaleBill[] saleList = ReadSaleBill();

            foreach (SaleBill sale in saleList)
            {
                if (sale.Id == id)
                {
                    return sale;
                }
            }

            return null;
        }

   
        public static SaleBill? SearchSaleBillByCreatedDay(DateOnly CreatedDate)
        {
            SaleBill[] saleList = ReadSaleBill();

            foreach (SaleBill sale in saleList)
            {
                if (sale.CreatedDate.Equals(CreatedDate))
                {
                    return sale;
                }
            }

            return null;
        }

        public static SaleBill? SearchSaleBillByDuration(DateOnly Mindate, DateOnly Maxdate)
        {
            SaleBill[] saleList = ReadSaleBill();

            foreach (SaleBill sale in saleList)
            {
                if (sale.CreatedDate >= Mindate && sale.CreatedDate <= Maxdate)
                {
                    return sale;
                }
            }

            return null;
        }

 
        public static bool AddNewSaleBill(SaleBill sale)
        {
            SaleBill[] saleList = ReadSaleBill();

            SaleBill[] newSaleList = new SaleBill[saleList.Length + 1];

            for (int i = 0; i < saleList.Length; i++)
            {
                newSaleList[i] = saleList[i];
            }

            newSaleList[newSaleList.Length - 1] = sale;

            SaveNewListToDB(newSaleList);

            return true;
        }


         
        public static void SaveNewListToDB(SaleBill[] list)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "salebill.txt");
            
            StreamWriter file = new StreamWriter(filePath);

            file.WriteLine(list.Length);

            for (int i = 0; i < list.Length; i++)
            {
                string detailIds = string.Join(",", list[i].detailBillsID);
                file.WriteLine($"{list[i].Id};{list[i].CreatedDate};[{detailIds}]");
            }
            file.Close();
        }
         
        public static SaleBill? EditSaleBill(SaleBill sb)
        {
            SaleBill[] list = ReadSaleBill();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id == sb.Id)
                {
                    list[i] = sb;

                    SaveNewListToDB(list);
                    return sb;
                }
            }

            return null;
        }


 
        public static void DeleteSaleBill(int id)
        {

            SaleBill[] list = ReadSaleBill();
            SaleBill[] newList = new SaleBill[list.Length - 1];

            int j = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id != id)
                {
                    newList[j] = list[i];
                    j++;
                }
            }
            SaveNewListToDB(newList);


        }
    }
}
