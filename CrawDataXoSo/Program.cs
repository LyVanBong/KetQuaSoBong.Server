using CrawDataXoSo.Helpers;

//// mien bac
//await XoSoMienBac.GetData();
//// mien nam
//await XoSoMienNam.GetData("https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", "KqxsMn");
//// mien trung
//await XoSoMienNam.GetData("https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", "KqxsMt");

string chon = "";
do
{
    Console.Clear();
    Console.WriteLine("-----------------------Crawl data xoso.me----------------------");
    Console.WriteLine("1. Xo So Mien Bac");
    Console.WriteLine("2. Xo So Mien Nam");
    Console.WriteLine("3. Xo So Mien Trung");
    Console.WriteLine("4. Lich bong da");
    Console.WriteLine("5. All");
    Console.WriteLine("0. Exit");
    chon = Console.ReadLine();
    switch (chon)
    {
        case "1":
            await XoSoMienBac.GetData();
            Console.WriteLine("Enter .... !");
            break;

        case "2":
            await XoSoMienNam.GetData("https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", "KqxsMn");
            Console.WriteLine("Enter .... !");
            break;

        case "3":
            await XoSoMienNam.GetData("https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", "KqxsMt");
            Console.WriteLine("Enter .... !");
            break;

        case "4":
            await LichBongDa.GetData();
            Console.WriteLine("Enter .... !");
            break;

        case "5":
            //await XoSoMienBac.GetData();
            //await XoSoMienNam.GetData("https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", "KqxsMn");
            //await XoSoMienNam.GetData("https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", "KqxsMt");
            Task.WaitAll(new Task[]
            {
                XoSoMienBac.GetData(),
                XoSoMienNam.GetData("https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", "KqxsMn"),
                XoSoMienNam.GetData("https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", "KqxsMt"),
                LichBongDa.GetData()
    });
            Console.WriteLine("Enter .... !");
            break;

        case "0":
            Console.WriteLine("Exit application");
            break;

        default:
            break;
    }
    Console.ReadLine();
} while (chon != "0");

//Task.WaitAll(new Task[]
//{
//    XoSoMienBac.GetData(),
//    XoSoMienNam.GetData("https://xoso.me/xsmn-{0}-ket-qua-xo-so-mien-nam-ngay-{0}.html", "KqxsMn"),
//    XoSoMienNam.GetData("https://xoso.me/xsmt-{0}-ket-qua-xo-so-mien-trung-ngay-{0}.html", "KqxsMt")
//});