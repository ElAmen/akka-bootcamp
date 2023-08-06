using System;
using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // initialize MyActorSystem
            // YOU NEED TO FILL IN HERE
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            // time to make your first actors!
            //YOU NEED TO FILL IN HERE
            // make consoleWriterActor using these props: Props.Create(() => new ConsoleWriterActor())
            // make consoleReaderActor using these props: Props.Create(() => new ConsoleReaderActor(consoleWriterActor))

            //var consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() =>new ConsoleWriterActor()), "consoleWriterActor");

            //var consoleReaderActor = MyActorSystem.ActorOf(Props.Create<ConsoleReaderActor>(consoleWriterActor), "consoleReaderActor"); 
            //// Lambda way of creating actors
            //// var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() =>new ConsoleReaderActor(consoleWriterActor)), "consoleReaderActor");

            //Generic way
            Props consoleWriteActorProps = Props.Create<ConsoleWriterActor>();
            var consoleWriterActor = MyActorSystem.ActorOf(consoleWriteActorProps, "consoleWriterActor");

            // Lambda way
            //Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            //var validationActor = MyActorSystem.ActorOf(validationActorProps, "validationActor");

            //var consoleReaderActorProps = Props.Create<ConsoleReaderActor>(validationActor);
            //var consoleReaderActor = MyActorSystem.ActorOf(consoleReaderActorProps, "consoleReaderActor");

            // make tailCoordinatorActor
            Props tailCoordinatorProps = Props.Create(() => new TailCoordinatorActor());
            IActorRef tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps, "tailCoordinatorActor");

            // pass tailCoordinatorActor to fileValidatorActorProps (just adding one extra arg)
            Props fileValidatorActorProps = Props.Create(() =>new FileValidatorActor(consoleWriterActor));
            IActorRef validationActor = MyActorSystem.ActorOf(fileValidatorActorProps, "validationActor");


            var consoleReaderActorProps = Props.Create<ConsoleReaderActor>();
            var consoleReaderActor = MyActorSystem.ActorOf(consoleReaderActorProps, "consoleReaderActor");
            // tell console reader to begin
            //YOU NEED TO FILL IN HERE
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }

    }
    #endregion
}
