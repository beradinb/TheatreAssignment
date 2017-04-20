using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace TheatreAssignment
{

    class Program
    {
        static void Main(string[] args)
        {

            Theatre nTheatre = new Theatre(2);

            ////////////////////RESERVING TEST/////////////////////
            //////////////////////////////////////////////////////
            Console.WriteLine("Reserving 50 seats of performance 1 ");
            for (int i =1; i <= 50; i++)
            {
                nTheatre.reserveSeat(i, 1, i);
            }
            nTheatre.printSeatingPlan(1);
            Console.WriteLine("{0} Seats available for performance 1", nTheatre.checkAvailability(1));
            Console.WriteLine(" ");

            Console.WriteLine("Reserving 100 seats of performance 2 ");
            for (int i = 1; i <= 100; i++)
            {
                nTheatre.reserveSeat(i, 2, i);
            }
            nTheatre.printSeatingPlan(2);
            Console.WriteLine("{0} Seats available for performance 2", nTheatre.checkAvailability(2));
            Console.WriteLine(" ");

            /////////////////////BUYING TEST//////////////////////
            //////////////////////////////////////////////////////
            Console.WriteLine("Buying 10 seats of performance 1");
            for (int i = 1; i <= 10; i++)
            {
                nTheatre.buySeat(i, 1, i);
            }
            nTheatre.printSeatingPlan(1);
            Console.WriteLine(" ");

            /////////////////////RETURN SEAT TEST/////////////////
            //////////////////////////////////////////////////////
            Console.WriteLine("Returning seat 11 of performance 1 that has been previously reserved");
            nTheatre.returnSeat(11, 1, 11);
            nTheatre.printSeatingPlan(1);
            Console.WriteLine(" ");

            /////////////////////CHECK CUSTOMER TEST//////////////
            //////////////////////////////////////////////////////
            Console.WriteLine("Checking who bought seat 1 of performance 1 that has been previously bought");
            Console.WriteLine("Customer ID {0} has bought seat 1", nTheatre.checkCustomer(1, 1));
            Console.WriteLine("Checking who reserved seat 15 of performance 1 that has been previously reserved");
            Console.WriteLine("Customer ID {0} has reserved seat 15", nTheatre.checkCustomer(1, 15));
            Console.WriteLine(" ");

            /////////////////////CANCEL RES TEST//////////////////
            //////////////////////////////////////////////////////
            Console.WriteLine("Cancelling reservations of performance 1");
            nTheatre.cancelReservations(1);
            nTheatre.printSeatingPlan(1);
            Console.WriteLine(" ");

            Console.ReadLine();

        }
    }

    /// <summary>
    /// Theatre Interface
    /// </summary>
    [ContractClass(typeof(TheatreContract))]
    public interface ITheatre
    {

        /// <summary>
        /// Dictionary which contains the performance key and a seat class array
        /// </summary>
        Dictionary<int, CSeat[]> dictionary { get; }
        /// <summary>
        /// Reserving a seat
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        bool reserveSeat(int customerId, int performanceId, int seatNum);
        /// <summary>
        /// Buying a seat
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        bool buySeat(int customerId, int performanceId, int seatNum);
        /// <summary>
        /// Returning a seat previously bought by a customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        bool returnSeat(int customerId, int performanceId, int seatNum);
        /// <summary>
        /// Theatre can cancel reservations
        /// </summary>
        /// <param name="performanceId"></param>
        void cancelReservations(int performanceId);
        /// <summary>
        /// Check availability of a performance
        /// </summary>
        /// <param name="performance"></param>
        /// <returns></returns>
        int checkAvailability(int performance);
        /// <summary>
        /// Check which customer has booked a seat for a performance by returning customer id from the seat
        /// </summary>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        int checkCustomer(int performanceId, int seatNum);
        /// <summary>
        /// Printing seat plan of a performance
        /// </summary>
        /// <param name="performanceId"></param>
        void printSeatingPlan(int performanceId);
    }

    /// <summary>
    /// Abstract class for the Theatre which contains the code contracts
    /// </summary>
    [ContractClassFor(typeof(ITheatre))]
    public abstract class TheatreContract : ITheatre
    {
        
        /// <summary>
        /// Dictionary which contains the performance key and a seat class array
        /// </summary>
        public Dictionary<int, CSeat[]> dictionary
        {
            get
            {
                return default(Dictionary<int, CSeat[]>);
            }
        }
        /// <summary>
        /// Buy Seats function which contains the contracts
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public bool buySeat(int customerId, int performanceId, int seatNum)
        {
            CSeat[] seat = dictionary[performanceId - 1];
            Contract.Requires(seatNum - 1 >= 0 && seatNum <= seat.Length, "Seat number must be in the range of 1-150");
            Contract.Requires(dictionary.ContainsKey(performanceId - 1));

            Contract.Ensures(true ? seat[seatNum - 1].sState == SeatState.Sold : seat[seatNum - 1].customerId != customerId);

            dictionary[performanceId - 1] = seat;
            return default(bool);
        }
        /// <summary>
        /// Cancel Reservation function which contains the contracts
        /// </summary>
        /// <param name="performanceId"></param>
        public void cancelReservations(int performanceId)
        {
            //Contract.Requires(dictionary[performanceId -1] != null);
            Contract.Requires(dictionary.ContainsKey(performanceId - 1));
            
        }
        /// <summary>
        /// Chech availability function with contracts
        /// </summary>
        /// <param name="performanceId"></param>
        /// <returns></returns>
        public int checkAvailability(int performanceId)
        {
            Contract.Requires(dictionary.ContainsKey(performanceId - 1));
            return default(int);
        }
        /// <summary>
        /// Check customer function with contracts
        /// </summary>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public int checkCustomer(int performanceId, int seatNum)
        {
            CSeat[] seat = dictionary[performanceId - 1];

            Contract.Requires(dictionary.ContainsKey(performanceId - 1));
            Contract.Requires(seatNum - 1 >= 0 && seatNum <= seat.Length, "Seat number must be in the range of 1-150");
            return default(int);
        }
        /// <summary>
        /// Print seating plan per performance function with contracts
        /// </summary>
        /// <param name="performanceId"></param>
        public void printSeatingPlan(int performanceId)
        {
            Contract.Requires(dictionary.ContainsKey(performanceId - 1));
        }
        /// <summary>
        /// Reserve seat function with contracts
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        public bool reserveSeat(int customerId, int performanceId, int seatNum)
        {
            CSeat[] seat = dictionary[performanceId - 1];
            Contract.Requires(dictionary.ContainsKey(performanceId - 1));
            Contract.Requires(seatNum - 1 >= 0 && seatNum <= seat.Length, "Seat number must be in the range of 1-150");
            Contract.Ensures(true ? seat[seatNum - 1].sState == SeatState.Reserved : seat[seatNum - 1].customerId != customerId);
            return default(bool);

        }
        /// <summary>
        /// Return seat function with contracts
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        public bool returnSeat(int customerId, int performanceId, int seatNum)
        {
            CSeat[] seat = dictionary[performanceId - 1];
            Contract.Requires(dictionary.ContainsKey(performanceId - 1));
            Contract.Requires(seatNum - 1 >= 0 && seatNum <= seat.Length, "Seat number must be in the range of 1-150");
            Contract.Ensures(true ? seat[seatNum-1].sState == SeatState.Available && seat[seatNum-1].customerId == 0 : seat[seatNum-1].customerId != 0);
            return default(bool);
        }
    }

    /// <summary>
    /// States for each seat
    /// </summary>
    public enum SeatState
    {
        /// <summary>
        /// Available state, seat can be booked.
        /// </summary>
        Available,
        /// <summary>
        /// Sold State, customer has bought this seat.
        /// </summary>
        Sold,
        /// <summary>
        /// Reserrved state, only customer who reserved this seat can buy it
        /// </summary>
        Reserved,
        /// <summary>
        /// Not in use state, can not be reserved or bought.
        /// </summary>
        NotInUse
    }

    /// <summary>
    /// Seat Class
    /// </summary>
    public class CSeat
    {
        /// <summary>
        /// Holds the customer ID so that the threatre know which seat was booked by which customer
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// Holds the state of each seat
        /// </summary>
        public SeatState sState { get; set; }


        /// <summary>
        /// Seat Constructor
        /// </summary>
        public CSeat()
        {
            customerId = 0;
            sState = SeatState.Available;
        }
    }

    /// <summary>
    /// Theatre Class, contains all the performances and functions to be carried out on a performance
    /// </summary>
    public class Theatre : ITheatre
    {
        
        int MAX_SEATS = 150;
        /// <summary>
        /// Invarients for max seats
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(MAX_SEATS == 150);
        }

        CSeat[] theatreSeat;
        Dictionary<int, CSeat[]> dictionary = new Dictionary<int, CSeat[]>();
        Dictionary<int, CSeat[]> ITheatre.dictionary
        {
            get
            {
                return dictionary;
            }
        }

        /// <summary>
        /// Theatre constructer which takes the number of performances
        /// </summary>
        /// <param name="performances"></param>
        public Theatre(int performances)
        {

            for (int j= 0; j < performances; j++)
            {

            theatreSeat = new CSeat[MAX_SEATS];
            for (int i = 0; i < MAX_SEATS; i++)
            {
                theatreSeat[i] = new CSeat(); 
            }
                dictionary.Add(j, theatreSeat);
            }

        }

        /// <summary>
        /// Displays the seating plan of a performance using the
        /// the performance ID
        /// </summary>
        /// <param name="performanceId"></param>
        public void printSeatingPlan(int performanceId)
        {
            CSeat[] seats = dictionary[performanceId - 1];

            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    switch(seats[count].sState)
                    {
                        case SeatState.Available:
                            Console.Write("A");
                            break;
                        case SeatState.NotInUse:
                            Console.Write("N");
                            break;
                        case SeatState.Reserved:
                            Console.Write("R");
                            break;
                        case SeatState.Sold:
                            Console.Write("S");
                            break;
                        default:
                            break;

                    }
                    count++;
                }
                Console.WriteLine();
            }
            count = 0;
            dictionary[performanceId - 1] = seats;
        }

        /// <summary>
        /// Buying a seat for a performance by chosing a seat number and passing customerID and performanceID
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public bool buySeat(int customerId, int performanceId, int seatNum)
        {
            seatNum = seatNum - 1;
            CSeat[] seats = dictionary[performanceId - 1];
            if ((seats[seatNum].sState == SeatState.Available) || (seats[seatNum].sState == SeatState.Reserved && seats[seatNum].customerId == customerId))
            {
                seats[seatNum].customerId = customerId;
                seats[seatNum].sState = SeatState.Sold;
                //Console.WriteLine("{0}, {1}, {2}", seats[seatNum].customerId, seatNum+1, seats[seatNum].sState);
                dictionary[performanceId - 1] = seats;
                return true;
            }
            else
            {
                Console.WriteLine("Already Booked");
                return false;
            }
            

            //seatNum = seatNum - 1;
            ////CSeat[] seats = dictionary[performanceId - 1];
            //if (((dictionary[performanceId - 1])[seatNum].sState == SeatState.Available) || ((dictionary[performanceId - 1])[seatNum].sState == SeatState.Reserved && (dictionary[performanceId - 1])[seatNum].customerId == customerId))
            //{
            //    (dictionary[performanceId - 1])[seatNum].customerId = customerId;
            //    (dictionary[performanceId - 1])[seatNum].sState = SeatState.Sold;
            //    Console.WriteLine("{0}, {1}, {2}", (dictionary[performanceId - 1])[seatNum].customerId, seatNum, (dictionary[performanceId - 1])[seatNum].sState);
            //}
            //else
            //{
            //    Console.WriteLine("Already Booked");
            //}



        }
        
        /// <summary>
        /// Cancelling all reservations for a performance
        /// </summary>
        /// <param name="performanceId"></param>
        public void cancelReservations(int performanceId)
        {
            CSeat[] seats = dictionary[performanceId-1];
            for (int i = 0; i < MAX_SEATS; i++)
            {
                if (seats[i].sState == SeatState.Reserved)
                {
                    seats[i].sState = SeatState.Available;
                }
            }
            dictionary[performanceId-1] = seats;
        }

        /// <summary>
        /// Checking the seat availability of a performance
        /// </summary>
        /// <param name="performanceId"></param>
        /// <returns></returns>
        public int checkAvailability(int performanceId)
        {
            CSeat[] seats = dictionary[performanceId-1];
            int seatCount = 0;
            for (int i = 0; i < MAX_SEATS; i++)
            {
                if (seats[i].sState == SeatState.Available)
                {
                    seatCount++;
                }
            }
            dictionary[performanceId-1] = seats;
            return seatCount;
        }

        /// <summary>
        /// Checking which which customer has booked a specific seat in a specific performance
        /// by returning customerID
        /// </summary>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        public int checkCustomer(int performanceId, int seatNum)
        {
            seatNum = seatNum - 1;
            CSeat[] seats = dictionary[performanceId-1];
            int cid = seats[seatNum].customerId;
            dictionary[performanceId-1] = seats;
            return cid;
        }

        /// <summary>
        /// Reserving a seat for a performance by chosing a seat number and passing customerID and performanceID 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        public bool reserveSeat(int customerId, int performanceId, int seatNum)
        {
            seatNum = seatNum - 1;
            CSeat[] seats = dictionary[performanceId-1];

            if (seats[seatNum].sState == SeatState.Available)
            {
                seats[seatNum].customerId = customerId;
                seats[seatNum].sState = SeatState.Reserved;
                //Console.WriteLine("{0}, {1}, {2}", seats[seatNum].customerId, seatNum+1, seats[seatNum].sState);
                return true;
            }
            dictionary[performanceId-1] = seats;
            return false;
        }

        /// <summary>
        /// Allows customer to return seat they have previously reserved
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="performanceId"></param>
        /// <param name="seatNum"></param>
        public bool returnSeat(int customerId, int performanceId, int seatNum)
        {
            seatNum = seatNum - 1;
            CSeat[] seats = dictionary[performanceId-1];

            if (seats[seatNum].sState == SeatState.Reserved && seats[seatNum].customerId == customerId)
            {
                seats[seatNum].customerId = 0;
                seats[seatNum].sState = SeatState.Available;
                //Console.WriteLine("{0}, {1}, {2}", seats[seatNum].customerId, seatNum+1, seats[seatNum].sState);
                return true;
            }
            dictionary[performanceId-1] = seats;
            return false;
        }
    }

}
