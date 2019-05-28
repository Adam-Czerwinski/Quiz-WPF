using MySql.Data.MySqlClient;

namespace Quiz.Source.DataAccessLayer
{
    class DBConnection
    {
        //statyczne pole istniejące od początku programu
        private static DBConnection instance = null;
        public static DBConnection Instance
        {
            get
            {
                //zwracamy referencję do obiektu tej klasy. Jeżeli taka referencja nie istnieje
                //to tworzymy nową instancję. Dzięki temu zawsze będzie istnieć tylko jedna referencja
                //tej klasy, do której dostęp będziemy mieć za pomocą statycznego propertiesa
                return instance?? (instance = new DBConnection());
            }
        }

        //prywatny set, żeby tylko w tej klasie móc nadać mu połączenie
        public MySqlConnection Connection { get; private set; }

        //SINGLETON wymaga prywatnego konstruktora
        private DBConnection()
        {
            //Tworzymy połączenie
            MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                //ustawiamy pola
                //DBInfo to nasza klasa, a po kropkach są to nazwy pól.
                UserID = DBInfo.user,
                Password = DBInfo.password,
                Database = DBInfo.database,
                Port = uint.Parse(DBInfo.port),
                Server = DBInfo.server
            };

            Connection = new MySqlConnection(connectionStringBuilder.ToString());
        }
    }
}
