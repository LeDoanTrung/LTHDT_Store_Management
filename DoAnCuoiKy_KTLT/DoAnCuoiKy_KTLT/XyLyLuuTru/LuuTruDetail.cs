using DoAnCuoiKy_KTLT.Entity;
using DoAnCuoiKy_KTLT.Entiy;

namespace DoAnCuoiKy_KTLT.XyLyLuuTru
{
    public class LuuTruDetail
    {
        
        public static DetailBill[] ReadDetailBill()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "detailbill.txt");
            StreamReader file = new StreamReader(filePath);

            int num = int.Parse(file.ReadLine());

            DetailBill[] list = new DetailBill[num];

            for (int i = 0; i < num; i++)
            {
                string[] categoryInfo = file.ReadLine().Split(';');
                list[i] = new DetailBill
                {
                    Id = int.Parse(detailInfo[0]),
                    productId = int.Parse(detailInfo[1]),
                    quantity = int.Parse(detailInfo[2])
                };
            }

            file.Close();

            return list;
        }

         
        public static DetailBill? SearchDetailBillById(int id)
        {
            DetailBill[] list = ReadDetailBill();

            foreach (DetailBill detail in list)
            {
                if (detail.Id == id)
                {
                    return detail;
                }
            }

            return null;
        }

        public static bool AddNewDetailBill(DetailBill detail)
        {
            DetailBill[] list = ReadDetailBill();

            DetailBill[] newList = new DetailBill [list.Length + 1];

            for (int i = 0; i < list.Length; i++)
            {
                newList[i] = list[i];
            }

            newList[newList.Length - 1] = detail;

            SaveNewListToDB(newList);

            return true;
        }

        public static void SaveNewListToDB(DetailBill[] list)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "detailbill.txt");
           
            StreamWriter file = new StreamWriter(filePath);

            file.WriteLine(list.Length);

            for (int i = 0; i < list.Length; i++)
            {
                file.WriteLine($"{list[i].Id};{list[i].productId};{list[i].quantity}");
            }
            file.Close();
        }


        public static DetailBill? EditDetailBill(DetailBill detail)
        {
            DetailBill[] list = ReadDetailBill();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Id == detail.Id)
                {
                    list[i].productId = detail.productId;
                    list[i].quantity = detail.quantity;

                    SaveNewListToDB(list);
                    return list[i];
                }
            }

            return null;
        }
        public static void DeleteDetailBill(int id)
        {

            DetailBill[] list = ReadDetailBill();
            DetailBill[] newList = new DetailBill[list.Length - 1];

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
