# WorkerBee
WorkerBee is a basic Windows Service meant to serve as a launchpad for running multiple automated tasks  asynchronously.

[NLog](https://nlog-project.org/) is installed via NuGet, configured, and ready to go. WorkerBee currently has one task configured as an example, once started this will randomly generate 2 integers and generate the sum of those integers - generated numbers, and the sum of those numbers are logged to files within the directory:

> logs/example-task/

Log entries from the base service itself are logged to the root of the directory:

> logs/

This directory can be found in the same directory as the WorkerBee executable.

## Getting It Up & Running

 1. Clone the repo to your local machine
 2. Open the solution
 3. Away you go

## Notes

 - When running WorkerBee via Visual Studio, the service will be launched in a Console Window, this allows for easier debugging, and console output of the logs via NLog. When installed as a service it will work like any other Windows service.
 - The example task that comes with WorkerBee will add 2 randomly generated integers every 5 minutes, and will continue to log as configured, unless you want logs of random sums on your computer, do not configure the service to run on start-up.
 - This project was built as a foundation for learning through examples, or as a launchpad for your own Windows Service. While this isn't an advanced project by any means, feel free to submit pull requests if you think it can be made more efficient.

