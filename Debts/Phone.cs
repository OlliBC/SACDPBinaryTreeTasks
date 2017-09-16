namespace Debts {

    public class Phone {
        
        public string Number { get; set; }
        public string CallRate { get; set; }

        public Phone(string number, string callRate) {
            Number = number;
            CallRate = callRate;
        }
        
    }

}