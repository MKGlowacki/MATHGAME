using database_test.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database_test.Data
{
    public class GameRepository
    {
        // w jednej zmiennej trzymamy sciezke do bazy danych, a druga jest do polaczenia z db
        string _dbPath;
        private SQLiteConnection conn;

        // konstruktor bierze sciezke do db i odpala metode inicjujaca polaczenie z db
        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
            Init();
        }

        public void Init()
        {
            conn = new SQLiteConnection( _dbPath );
            conn.CreateTable<Game>();
        }

        //wyswietlanie danych wszystkich rozegranych gier
        public List<Game> GetAllGames() 
        {
          
            return conn.Table<Game>().ToList();
        }

        //dodawanie danych na temat rozgrywki do db
        public void Add(Game game )
        {
          
            conn = new SQLiteConnection(_dbPath);
            conn.Insert( game );
        }

        //usuwanie danych na temat rozgrywki do db
        public void Delete(int id)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Game { Id = id});
        }
    }
}
