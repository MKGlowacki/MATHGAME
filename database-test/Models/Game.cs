using SQLite;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace database_test.Models
{
    //tabela game z odpowiednimi kolumnami, Id jest primarykey z autoinkrementacja
    [Table("game")]
    public class Game
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public GameOperation Type { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
        public string Difficulty { get; set; }

        
    }
    public enum GameOperation
    {
        Addition,
        Substraction,
        Multiplication,
        Division,
    }
}
