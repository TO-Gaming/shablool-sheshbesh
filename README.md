<div dir='rtl' lang='he'>

# shablool-sheshbesh

**אזהרה - משחק ממכר**
https://to-gaming.itch.io/shablul-backgammon (Beta)
  
#  Scenes Prefabs and Scripts:
  
  ## Scenes:
  Lobby - in the lobby the player can choose if he wants to start or to do the tutorial (not implemented yet).<br />
  SampleScene (Main Game) - the main scene of the game.<br />
  Tutorial - in this scene the player is about to understand the rules of Shablul-Sheshbesh.<br />
  
  ## Prefabs:
  WhitePiece - represents one white soldier (Dragable) .<br />
  CustomTile - the editor can configure the contents of each Tile.<br />
  GroupTiles - a prefab that holds 6 Customizable Tiles next to each other (4 pieces in game board) <br /> 
  Dice (toQuit) - in Game option to exit to Lobby .<br />
  Dice (toStart) - in Game option to Start the Game.<br />
  Dice (toRoll) - grey cylinder Object to trigger Roll .<br />
  
  ## Scripts and effects:
  AddRemove.cs - Makes every Tile (24 tiles in game) be able to instanciate 'Soldiers'(Black or White) along the Tile.<br />
  DragObject.cs - makes evry 'soldier' moveable by calculating x and z from mouse to soldier.<br />
  DiceResults.cs - Sets new Dice values (and shake the dices) , visable on screen.<br />
  Dice (Physics material) - bounciness to the required objects.
## מהות המשחק

 שבלול שש בש זה משחק אשר מאפשר חוויה הרבה יותר אינטנסיבית כי מדובר בגרסא שונה לחלוטין שמציעה למשתמש גם אופציות שונות תוך כדי המשחק, המשחק עצמו הוא מתחיל כמו שש בש אבל כאשר אתה הורג חייל של האויב הוא יוצא מהמשחק, ואתה מקבל על זה קרדיט, או כאשר השחקן מתקדם ועובר את ה"מעבר להתחלה", ואז בקרדיט שהרוויח יכול לרכוש "קלף כוח" שאיתו יוכל להשפיע על המשחק.
המשחק מיועד לפלאפון או לאינטרנט

 
 <img src="screenshablul.jpg" width="150" title="Lobby screen">
 


---
## ScreenShots
<img src="ShablulGameplay.jpg" width="550" title="hover text"> 


## רכיבים רשמיים


### 1. שחקנים

* המשחק מיועד לילדים ומעלה, ובעיקר אך לא בהכרח לשחקני שש בש
* במשחק 2 משתתפים אך ניתן יהיה לשחק ברשת נגד אנשים שונים או באותו מכשיר
* משחק תחרותי, הטוב ביותר ינצח

### 2. יעדים

* היעד במשחק הוא להרוג את כל חייליו של היריב עד לסיום המשחק
* בתחילת המשחק יהיה הסבר פשוט אשר ינחה את השחקן שעליו להרוג כמה שיותר חיילים ויקבל חיזוק עם אופצית מימוש במידה והצליח 
* אמנם יש יעד בודד אבל שחקן יוכל לסיים את המשחק בעזרת אסטרטגיה של התקדמות מהירה של חילים
 
### 3. תהליכים


* השחקן שנכנס למשחק רואה תמונות וחלקים מ"יכולות העל" שהמשחק מציע, נראה כאילו הולך לשחק שש בש אך סקרן לדעת איך יוכל לבטא את כישורי המשחק שלו במשחק שהוא שונה במהותו  
*	התהליך המרכזי הוא להתקדם בעזרת הקוביות תוך כדי בניית בתים מבלי להיות מותקף על ידי היריב הבעיה היא שחוץ מקוביות, עוד אלמנט שקשור במזל במשחק הוא קלפי כוח שונים שיכול לממש אותם במשחק וישפיעו על הקרב. 
 (לדוגמא קלף קוביות מזל - ולקבל אופציה גבוהה לדאבל, קלף יכולת הריסת בית בגודל 2, קלף הגנה עבור ניסיון אחד של האויב להתקפה, קלף רוח שבוחר 3 חיילים וממקם אותם בצורה אקראית מחדש)
*	סיום המשחק ייקבע לפי השחקן שיצליח להותיר לאויב 3 חיילים בלבד או שהמשחק הגיע לסוף הזמן ויחושב למי יש יותר חיילים
* יהיה שימוש בעיקר במגע בשביל להחליט איך להזיז את החיילים, ועיון בחנות יתבצע גם כך

השחקן מתחיל לשחק משחק שהוא מכיר, ומקבל הדרכה תוך כדי וגם פידבק טוב על הריגת חיילי האויב, עד שמתרגל ומייצר לעצמו אסטרטגיות חדשות תואמות ומתקדם עם הקלפים המיוחדים והשונים 


### 4. חוקים

* ניתן לכל שחקן זריקת קוביות אחת, תוצאת הקוביות תחליט איך יוכל להתקדם
* במידה ושחקן מתקדם לעבר חייל ללא בית יוכל להוציאו מהמשחק
* החוקים יתוארו במשחק הראשון של השחקן


### 5. משאבים

  <img src="https://cdn3.iconfinder.com/data/icons/game-icon-set-rounded/512/cart-512.png" width="350" title="hover text">
  <img src="https://i.pinimg.com/736x/a9/07/61/a90761a6cf9aa4c8efafe0f7b7db9765.jpg" width="350" title="hover text">
 
* (1)המשאבים במשחק הם המטבעות-שבלול שהשחקן יצבור בהריגת חייל(2) , ובנוסף קרדיט עבור מעברים ב"שער" אל עבר תחילת המגרש
* ניתן להחליף מטבעות שבלול ביכולות בחנות המשחק שיעזרו למתמודד לנצח
 * בנוסף ניתן להחליף מטבעות עבור קלפי כוח 
 * איך השחקן יוכל להשיג משאבים?
 * הריגת חיילים מסויימים יביאו לך 2 מטבעות שבלול
 * במעבר של החיילים מסוף המגרש לתחילתו נצברים מטבעות
* ברגע ששחקן מגיע למעברים הוא יקבל הודעה רשמית ויפה על קבלת המטבע, ובמידה ויש לו מספיק לרכישה יקבל עצה איך יוכל להשתמש בהם

  <img src="https://cdn0.iconfinder.com/data/icons/game-elements-3/64/card-life-power-cards-game-element-gift-512.png" width="350" title="hover text">
 
### 6. עימותים

מה יהיו העימותים המרכזיים במשחק:

* יש עימות מרכזי בין שני המשתתפים, שבו הם מנסים להתגונן על ידי יצירת בתים והתקדמות עם נסיון לכמה שפחות סיכונים, עד שהמזל מתערב
* דילמה שקיימת במהלך המשחק בין השחקן לעצמו היא אופציות ההגנה השונות, ובנוסף בגרסאת הארקייד יש אופציה עבור השחקן להחליט איך משקיע את הנקודות שלו בכדי להלחם באויב


### 7. גבולות
זהו משחק קופסא ולכן משחקים בתוכו בתסנית מוכרת.
גבולות המשחק הם כמו שש בש, רק שמותר להתקדם חופשי מסוף המגרש לתחילתו עבור כל שחקן וכיוון המשחק שלו.



### 8. תוצאות

התוצאות האפשריות הן נצחון שלך או של היריב, או לחלופין תיקו במקרה שהמשחק הגיע לסופו בזמן שכמות החיילים של שני המשתתפים זהה 
* המשחק בדומה לשש בש מורכב גם ממזל וגם מטכניקה, על מנת לנצח מסל לא יספיק לכם, ובנוסף ללא מזל ואך ורק בעזרת תכנון טוב אפשר להגיע לנצחון 
* האם המשחק יהיה "סכום אפס", שיתופי, או מורכב?

---

## סקר שוק

 למשחק אין מתחרים שמציעים משחק שש בש ארקייד בעל תכונות שונות ומגוונות, אלא רק גרסאות קיימות מהעולם של המשחק הקלאסי שש בש
 
 1. Backgammon Games : 18
 *https://play.google.com/store/apps/details?id=com.tavla5
  <img src="https://play-lh.googleusercontent.com/0fosTnVf57Tzm73TvXvaz_SI5Tx93ftm2eT2sLZQUZCSQKMfKiLhqskw8zDEh88J4A=s360-rw" width="200" title="hover text">
 
 2. Backgammon - Lord of the Board
 * משחק שש בש קלאסי עם הימורים על כסף, משחק שונה לגמריי ולא ייחודי
 * https://play.google.com/store/apps/details?id=air.com.beachbumgammon
 <img src="https://play-lh.googleusercontent.com/4Me5eScbbiql32uaGJ4sLtGcP3KFWksQPEmHb8Ht0xf7j963K9bls6s4Cx4X8IThUBQ=s360-rw" width="200" title="hover text">
 
 
 
 3.PlayGem Backgammon
 * המשחק עובד בלייב אבל הוא מציע משחק רגיל של שש בש
 * https://play.google.com/store/apps/details?id=playgem.backgammon
 <img src="https://play-lh.googleusercontent.com/m_9oYvPrFYo1ZMlJY6mcBe3NKj5-0vrngocnh9j9WBttaWn9MI2FFfKIx1Il1S8vSs4=s360-rw" width="200" title="hover text">
 
 
לפני שמתחילים לעבוד על משחק (או כל מוצר אחר), חשוב לוודא שלא עשו את זה קודם. לא נעים לעבוד סמסטר שלם (או שנה שלמה) על משחק ואז לגלות שכבר יש משחק כזה. 

חפשו בגוגל, בחנות play, בפייסבוק, ובכל מקום אחר שיש לכם גישה אליו, משחקים דומים לרעיון שלכם. ציינו באיזה ביטויי-חיפוש השתמשתם.

זהו את שלושת המשחקים הדומים ביותר. לגבי כל אחד מהם:

* שימו קישור וצילום-מסך להמחשה.
* הסבירו מה תעשו כדי שהמשחק שלכם יהיה שונה/מקורי/מיוחד/טוב יותר מהמתחרים?  מדוע שחקנים יעדיפו דווקא את המשחק שלכם?

מבין הרכיבים הרשמיים, 
איזה רכיב (או רכיבים) ידגיש ביותר את הייחוד והמקוריות של המשחק שלכם, לעומת משחקים דומים הקיימים בשוק?


</div>
