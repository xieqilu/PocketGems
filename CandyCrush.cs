7 Candy Crush
class GridPoint {
   int x;
   int y;
}

class Item {
   Color color;
}

class Match {
   GridPoint[] matchPoints;
   void removeMatchPoints();
   void addMatchPoint(GridPoint point);
}

class Game {
   Item[][] board;
   int boardWidth;
   int boardHeight;
   int currentScore;

   // this is the callback for user interaction
   void swap(GridPoint pointA, GridPoint pointB) {
       // points A and B will be adjacent
       Item originalItemAtPointA = board[pointA.x][pointA.y];
       board[pointA.x][pointA.y] = board[pointB.x][pointB.y];
       board[pointB.x][pointB.y] = originalItemAtPointA;
       List<Match> matches = getMatches();
       if (matches.length == 0) {
           board[pointB.x][pointB.y] = board[pointA.x][pointA.y];
           board[pointA.x][pointA.y] = originalItemAtPointA;
       }
       for (Match match in matches) {
           // match: 0,2; 0,1; 0,0

           boolean horizontal = false;
           boolean start = true;
           int x = 0, y = 0;
           int boundary = 0;
           for (GridPoint matchPoint in match.matchPoints) {
               if(start){
                   start = false;
                   x = matchPoint.x;
                   y = matchPoint.y;
               }else{
                   if(matchPoint.x != x){
                       horizontal = true;
                       break;
                   }
                   if(matchPoint.y > y){
                       boundary = y;
                       break;
                   }else y = matchPoint.y;
               }
           }

           int index = 0;
           for (GridPoint matchPoint in match.matchPoints) {
               board[matchPoint.x][matchPoint.y] = null;
               currentScore++;
               if(horizontal){
                   index = matchPoint.y;
                   while(index > 0){
                       board[matchPoint.x][matchPoint.y] =
board[matchPoint.x][index - 1];
                       index--;
                   }
                   board[matchPoint.x][0] = randomIterm();
               }
           }

           if(!horizontal){
               while(index >= boundary){
                       board[matchPoint.x][matchPoint.y] =
board[matchPoint.x][index - 1];
                       index--;
                   }
                   board[matchPoint.x][0] = randomIterm();
           }
       }
       if (currentScore >= 3) {
           win(currentScore);
       }
   }

   List<Match> getMatches() {
       List<Match> foundMatches = new List<Match>();
       foundMatches.append(getHorizontalMatches());
       foundMatches.append(getVerticalMatches());
       return foundMatches;
   }

   List<Match> getHorizontalMatches() {
       List<Match> foundMatches = new List<Match>();
       Match match = new Match();
       for (int y = 0; y < boardHeight; y++) {
           int x = 0;
           GridPoint startingGridPoint = nil;
           Item startingItem = nil;
           match.removeMatchPoints();
           while (x < boardWidth) {
               if (x < boardWidth && startingGridPoint == nil) {
                   startingGridPoint = new GridPoint(x, y);
                   startingItem = board[x][y];
                   if (startingItem) {
                       match.addMatchPoint(startingGridPoint);
                   }
                   x++;
               } else {
                   GridPoint nextGridPoint = new Pair(x, y);
                   Item nextItem = board[x][y];
                   if (startingItem && nextItem &&
nextItem.color.isEqual(startingItem.color)) {
                       match.addMatchPoint(nextGridPoint);
                       x++;

                   }else if(startingItem && nextItem &&
!nextItem.color.isEqual(startingItem.color)){
                       if (match.matchPoints.length >= 3) {
                           foundMatches.append(match);
                       }
                       match = new Match();
                       startingGridPoint = nil;
                       startingItem = nil;
                   }
                   else {
                       match = new Match();
                       startingGridPoint = nil;
                       startingItem = nil;
                   }
               }
           }
           if (match.matchPoints.length >= 3) {
               foundMatches.append(match);
           }
       }
       return foundMatches;
   }

   List<Match> getVerticalMatches() {
       ...
   }
}

1) This implements a simple game. What is the name of the game, or if
you don't know, how does the game work?

http://bejeweled.popcap.com/html5 (Candy Crush)

2) Right now the game only handles matches of exactly 3. Please extend
the solution to find the longest
matches possible with a minimum of 3.

3) Extend this to automatically drop gems (1) This implements a simple game. What is the name of the game, or if
you don't know, how does the game work?
