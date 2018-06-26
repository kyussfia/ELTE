﻿using ELTE.TicTacToeGame.Persistence;
using System;
using System.Linq;

namespace ELTE.TicTacToeGame.Model
{
    /// <summary>
    /// Tic-Tac-Toe játék modell típusa.
    /// </summary>
    public class TicTacToeModel
    {
        #region Private fields

        private Player _currentPlayer; // aktuális játékos
        private Player[,] _gameTable; // játéktábla
        private Int32 _stepNumber; // lépésszám
        private IPersistence _persistence; // adatelérés

        #endregion

        #region Public properties

        /// <summary>
        /// Lépésszám lekérdezése.
        /// </summary>
        public Int32 StepNumber { get { return _stepNumber; } }

        /// <summary>
        /// Aktuális játékos lekérdezése.
        /// </summary>
        public Player CurrentPlayer { get { return _currentPlayer; } }

        /// <summary>
        /// Mezőérték lekérdezése, vagy beállítása.
        /// </summary>
        /// <param name="x">Oszlop index.</param>
        /// <param name="y">Sor index.</param>
        /// <returns>A mező értéke.</returns>
        public Player this[Int32 x, Int32 y] 
        { 
            get 
            {
                if (x < 0 || x > _gameTable.GetLength(0)) // ellenőrizzük a tartományt
                    throw new ArgumentException("Bad column index.", "x");
                if (y < 0 || y > _gameTable.GetLength(1))
                    throw new ArgumentException("Bad row index.", "y");

                return _gameTable[x, y]; 
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Játék megnyerésének eseménye.
        /// </summary>
        public event EventHandler<GameWonEventArgs> GameWon;

        /// <summary>
        /// Játék végének eseménye.
        /// </summary>
        public event EventHandler GameOver;

        /// <summary>
        /// Mezőváltozás eseménye.
        /// </summary>
        public event EventHandler<FieldChangedEventArgs> FieldChanged;


        #endregion

        #region Constructors

        /// <summary>
        /// Tic-Tac-Toe játék modell példányosítása.
        /// </summary>
        public TicTacToeModel() : this(null) { }

        /// <summary>
        /// Tic-Tac-Toe játék modell példányosítása.
        /// </summary>
        /// <param name="dataAccess">Az adatelérés.</param>
        public TicTacToeModel(IPersistence dataAccess) 
        {
            _gameTable = new Player[3, 3]; // mátrix létrehozása
            _persistence = dataAccess;

            NewGame();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Új játék indítása.
        /// </summary>
        public void NewGame() 
        { 
            for (Int32 i = 0; i < _gameTable.GetLength(0); i++) // végigmegyünk a mátrix elemein
                for (Int32 j = 0; j < _gameTable.GetLength(1); j++)
                {
                    _gameTable[i, j] = Player.NoPlayer; // a játékosok pozícióit töröljük
                }

            _stepNumber = 0;
            _currentPlayer = Player.PlayerX; // először az X lép
        }

        /// <summary>
        /// Játék léptetése.
        /// </summary>
        /// <param name="x">Oszlop index.</param>
        /// <param name="y">Sor index.</param>
        public void StepGame(Int32 x, Int32 y) 
        {
            if (x < 0 || x > _gameTable.GetLength(0)) // ellenőrizzük a tartományt
                throw new ArgumentOutOfRangeException("x", "Bad column index.");
            if (y < 0 || y > _gameTable.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "Bad row index.");
            if (_stepNumber >= 9) // ellenőrizzük a lépésszámot
                throw new InvalidOperationException("Game is over!");
            if (_gameTable[x, y] != Player.NoPlayer) // ellenőrizzük a mezőt
                throw new InvalidOperationException("Field is not empty!");

            _gameTable[x, y] = _currentPlayer; // pozíció rögzítése
            OnFieldChanged(x, y, _currentPlayer); // jelezzük egy eseménykiváltással, hogy változott a mező

            _stepNumber++;
            _currentPlayer = _currentPlayer == Player.PlayerO ? Player.PlayerX : Player.PlayerO;
            // beállítjuk a következő játékost

            CheckGame();
        }

        /// <summary>
        /// Játék betöltése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public void LoadGame(String path)
        {
            if (_persistence == null)
                return;

            // végrehajtjuk a betöltést
            Player[] values = _persistence.Load(path);

            if (values.Length != _gameTable.Length)
                throw new DataException("Error occured during game loading.");

            if (values.Count(value => value == Player.PlayerO) != values.Count(value => value == Player.PlayerX) && values.Count(value => value == Player.PlayerO) + 1 != values.Count(value => value == Player.PlayerX))
                // ellenőrizzük, hogy megfelelő számú lépés történt-e a játékosok részéről (leszámoljuk, hány lépés történt az egyes játékosok részéről)
                throw new DataException("Error occured during game loading.");

            _stepNumber = values.Count(value => value != Player.NoPlayer); // leszámoljuk, hány lépés történt
            _currentPlayer = _stepNumber % 2 == 0 ? Player.PlayerX : Player.PlayerO; // a lépésszámból kijön az aktuális játékos

            // beállítjuk az értékeket
            for (Int32 i = 0; i < _gameTable.GetLength(0); i++)
                for (Int32 j = 0; j < _gameTable.GetLength(1); j++)
                {
                    _gameTable[i, j] = values[i * _gameTable.GetLength(0) + j];

                    OnFieldChanged(i, j, _gameTable[i, j]);
                }

            CheckGame();
        }

        /// <summary>
        /// Játék mentése.
        /// </summary>
        /// <param name="path">Elérési útvonal.</param>
        public void SaveGame(String path)
        {
            if (_persistence == null)
                return;

            // az értékeket kimásoljuk egy új tömbbe
            Player[] values = new Player[_gameTable.Length];
            for (Int32 i = 0; i < _gameTable.GetLength(0); i++)
                for (Int32 j = 0; j < _gameTable.GetLength(1); j++)
                {
                    values[i * _gameTable.GetLength(0) + j] = _gameTable[i, j];
                }

            // végrehajtjuk a mentést
            _persistence.Save(path, values);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Játék ellenőrzése.
        /// </summary>
        private void CheckGame() 
        {
            Player won = Player.NoPlayer;

            for (int i = 0; i < 3; ++i) // ellenőrzések végrehajtása
            {
                if (_gameTable[i, 0] != 0 && _gameTable[i, 0] == _gameTable[i, 1] && _gameTable[i, 1] == _gameTable[i, 2])
                    won = _gameTable[i, 0];
            }
            for (int i = 0; i < 3; ++i)
            {
                if (_gameTable[0, i] != 0 && _gameTable[0, i] == _gameTable[1, i] && _gameTable[1, i] == _gameTable[2, i])
                    won = _gameTable[0, i];
            }
            if (_gameTable[0, 0] != 0 && _gameTable[0, 0] == _gameTable[1, 1] && _gameTable[1, 1] == _gameTable[2, 2])
                won = _gameTable[0, 0];
            if (_gameTable[0, 2] != 0 && _gameTable[0, 2] == _gameTable[1, 1] && _gameTable[1, 1] == _gameTable[2, 0])
                won = _gameTable[0, 2];

            if (won != Player.NoPlayer) // ha valaki győzött
            {
                OnGameWon(won); // esemény kiváltása
            }
            else if (_stepNumber == 9) // döntetlen játék
            {
                OnGameOver(); // esemény kiváltása
            }
        }

        #endregion

        #region Event triggers

        /// <summary>
        /// Játék megnyerésének eseménykiváltása.
        /// </summary>
        /// <param name="player">A győztes játékos.</param>
        private void OnGameWon(Player player)
        {
            if (GameWon != null)
                GameWon(this, new GameWonEventArgs(player));
        }
        /// <summary>
        /// Játék végének eseménykiváltása.
        /// </summary>
        private void OnGameOver()
        {
            if (GameOver != null)
                GameOver(this, EventArgs.Empty);
        }
        /// <summary>
        /// Mezőváltozás eseménykiváltása.
        /// </summary>
        /// <param name="x">Oszlop index.</param>
        /// <param name="y">Sor index.</param>
        /// <param name="player">Játékos.</param>
        private void OnFieldChanged(Int32 x, Int32 y, Player player)
        {
            if (FieldChanged != null)
                FieldChanged(this, new FieldChangedEventArgs(x, y, player));
        }

        #endregion
    }
}
