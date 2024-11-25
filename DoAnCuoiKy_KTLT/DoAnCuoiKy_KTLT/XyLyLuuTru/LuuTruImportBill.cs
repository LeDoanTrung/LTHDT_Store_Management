using DoAnCuoiKy_KTLT.Entiy;

namespace DoAnCuoiKy_KTLT.XyLyLuuTru
{
    public class LuuTruImportBill
    {
        public static ImportBill[] ReadImportBill()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "importbill.txt");
            StreamReader file = new StreamReader(filePath);

            int numImports = int.Parse(file.ReadLine());

            ImportBill[] ImportList = new ImportBill[numImports];

            for (int i = 0; i < numImports; i++)
            {
                string[] billInfo = file.ReadLine().Split(';');

                ImportList[i].Id = int.Parse(billInfo[0]);
                ImportList[i].CreatedDate = DateOnly.Parse(billInfo[1]);

                string detailIDsString = billInfo[2].Trim('[', ']');
                if (string.IsNullOrWhiteSpace(detailIDsString))
                {
                    // Nếu rỗng, gán một mảng rỗng cho detailBillsID
                    ImportList[i].detailBillsID = new int[0];
                }
                else
                {
                    string[] detailIDs = detailIDsString.Split(',');
                    ImportList[i].detailBillsID = Array.ConvertAll(detailIDs, int.Parse);
                }
            }

            file.Close();

            return ImportList;
        }


        public static ImportBill? SearchImportBillById(int id)
        {
            ImportBill[] ImportList = ReadImportBill();

            foreach (ImportBill Import in ImportList)
            {
                if (Import.Id == id)
                {
                    return Import;
                }
            }

            return null;
        }


        public static ImportBill? SearchImportBillByCreatedDay(DateOnly CreatedDate)
        {
            ImportBill[] ImportList = ReadImportBill();

            foreach (ImportBill Import in ImportList)
            {
                if (Import.CreatedDate.Equals(CreatedDate))
                {
                    return Import;
                }
            }

            return null;
        }

        public static ImportBill? SearchImportBillByDuration(DateOnly Mindate, DateOnly Maxdate)
        {
            ImportBill[] ImportList = ReadImportBill();

            foreach (ImportBill Import in ImportList)
            {
                if (Import.CreatedDate >= Mindate && Import.CreatedDate <= Maxdate)
                {
                    return Import;
                }
            }

            return null;
        }


        public static bool AddNewImportBill(ImportBill Import)
        {
            ImportBill[] ImportList = ReadImportBill();

            ImportBill[] newImportList = new ImportBill[ImportList.Length + 1];

            for (int i = 0; i < ImportList.Length; i++)
            {
                newImportList[i] = ImportList[i];
            }

            newImportList[newImportList.Length - 1] = Import;

            SaveNewListToDB(newImportList);

            return true;
        }



        public static void SaveNewListToDB(ImportBill[] list)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string filePath = Path.Combine(projectRoot, "resource", "importbill.txt");
           
            StreamWriter file = new StreamWriter(filePath);

            file.WriteLine(list.Length);

            for (int i = 0; i < list.Length; i++)
            {
                string detailIds = string.Join(",", list[i].detailBillsID);
                file.WriteLine($"{list[i].Id};{list[i].CreatedDate};[{detailIds}]");
            }
            file.Close();
        }

        public static ImportBill? EditImportBill(ImportBill sb)
        {
            ImportBill[] list = ReadImportBill();

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



        public static void DeleteImportBill(int id)
        {

            ImportBill[] list = ReadImportBill();
            ImportBill[] newList = new ImportBill[list.Length - 1];

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
