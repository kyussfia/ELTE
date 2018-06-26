using System;
using System.IO;
using System.Threading.Tasks;
using ELTE.Sudoku.Droid.Persistence;
using ELTE.Sudoku.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDataAccess))]
namespace ELTE.Sudoku.Droid.Persistence
{
    /// <summary>
    /// Tic-Tac-Toe adatel�r�s megval�s�t�sa Android platformra.
    /// </summary>
    public class AndroidDataAccess : ISudokuDataAccess
    {
        /// <summary>
        /// F�jl bet�lt�se.
        /// </summary>
        /// <param name="path">El�r�si �tvonal.</param>
        /// <returns>A beolvasott mez��rt�kek.</returns>
        public async Task<SudokuTable> Load(String path)
        {
            // a bet�lt�s a szem�lyen k�nyvt�rb�l t�rt�nik
            String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);

            // a f�jlm�veletet taszk seg�ts�g�vel v�gezz�k (aszinkron m�don)
            String[] values = (await Task.Run(() => File.ReadAllText(filePath))).Split(' ');
            
            Int32 tableSize = Int32.Parse(values[0]);
            Int32 regionSize = Int32.Parse(values[1]);
            SudokuTable table = new SudokuTable(tableSize, regionSize); // l�trehozzuk a t�bl�t

            Int32 valueIndex = 2;
            for (Int32 rowIndex = 0; rowIndex < tableSize; rowIndex++)
            {              
                for (Int32 columnIndex = 0; columnIndex < tableSize; columnIndex++)
                {
                    table.SetValue(rowIndex, columnIndex, Int32.Parse(values[valueIndex]), values[valueIndex] != "0"); // �rt�kek bet�lt�se
                    valueIndex++;
                }
            }

            return table;
        }

        /// <summary>
        /// F�jl ment�se.
        /// </summary>
        /// <param name="path">El�r�si �tvonal.</param>
        /// <param name="table">A f�jlba ki�rand� j�t�kt�bla.</param>
        public async Task Save(String path, SudokuTable table)
        {
            String text = table.Size.ToString() + " " + table.RegionSize.ToString(); // m�ret

            for (Int32 i = 0; i < table.Size; i++)
            {
                for (Int32 j = 0; j < table.Size; j++)
                {
                    text += table[i, j] + " "; // mez��rt�kek
                }
            }

            // f�jl l�trehoz�sa
            String filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);

            // ki�r�s (aszinkron m�don)
            await Task.Run(() => File.WriteAllText(filePath, text));
        }
    }
}