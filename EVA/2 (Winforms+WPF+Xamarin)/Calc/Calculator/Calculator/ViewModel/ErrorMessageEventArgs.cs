using System;

namespace ELTE.Calculator.ViewModel
{
    /// <summary>
    /// Hibaüzenet eseményargumentum típusa.
    /// </summary>
    public class ErrorMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Üzenet lekérdezése.
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// Hibaüzenet eseményargumentum példányosítása.
        /// </summary>
        /// <param name="message">Üzenet.s</param>
        public ErrorMessageEventArgs(String message)
        {
            Message = message;
        }
    }
}
