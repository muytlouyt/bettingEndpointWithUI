Hello!
- Place Bet Endpoint:
  1. Done in BetsController -> MakeBet method.
- Validation: 
  1. Player validation - I decided to use normal authentication process to validate player, so player = user, and the person who makes bets should be in the session. On the endpoint side there is also simple checking.
  2. Event validation - In the case of accessing endpoint from UI player can choose only existing events, that's my approach. On the endpoint side there is also simple checking.
  3. Validate odds - Tolerance checking is done, there is error message.
- Read/Write to JSON files:
  1. Done in Service classes which use JsonHelper
- Get All Bets Logs Endpoint:
  1. Done in BetsController -> GetBets method.
- Submission:
  1. Provide a GitHub repository with your .NET Web API project - done as you see
  2. Write unit tests for key functionalities - not done, because I'm not experienced in testing so decided to focus more on other parts
  3. Demonstrate your approach for handling edge cases and errors - system shows beatiful error messages
