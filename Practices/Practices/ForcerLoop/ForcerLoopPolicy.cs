namespace Practices.ForcerLoop
{
    public class ForcerLoopPolicy
    {
        public bool RetryOnFail { get; set; }

        public int RetryCount { get; set; }


        public int RetryIntervalAsSeconds
        {
            get { return (_retryIntervalAsMiliseconds / 1000); }
            set { _retryIntervalAsMiliseconds = value * 1000; }
        }

        private int _retryIntervalAsMiliseconds = 60 * 1000;

        public int RetryIntervalAsMiliseconds
        {
            get { return _retryIntervalAsMiliseconds; }
            set { _retryIntervalAsMiliseconds = value; }
        }
    }
}