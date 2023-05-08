/*
 * Filename: dsA3
 * By: Justin Fink
 * Date: April,16 2021
 * Description: This function gets movie genres, titles and ratings
 *				from the user, then it puts them into two multimaps, 
 *				then the program prints off all the genres and titles neatly.
 *				The program then asks the user to input a title and name of one
 *				of the movies they have already entered to change the rating value.
 *				the change then gets updated in the multimaps by deleting and reinserting
 *				then it prints again. Then the program is finished.
 *
 */

#include<iostream>
#include <string>
#include <vector>
#include <map>
#pragma warning(disable:4996)

using namespace std;

//Prototypes
const char* nTakeaway(char* str);
int getNum(void);
int check(string);
string space(int size);
void printMultimap(multimap<int, string>IntegerMultimap, multimap<string, int> StringMultimap);
void deletion(multimap<int, string>IntegerMultimap, multimap<string, int> StringMultimap, string bothGenreTitle);

int main()
{
	int periodCheck = 1;
	int size = 0;
	string spaces;

	string genre;
	string title;
	int rating = 0;
	string bothGenreTitle;

	multimap<int, string> IntegerMultimap;
	multimap<string, int> StringMultimap;

	while (periodCheck != 0)
	{
		//promt user to input genre/movie/rating
		//Get Genre
		cout << "Enter the movie genre: ";
		getline(cin, genre);
		periodCheck = check(genre);
			
		//Get Title
		if (periodCheck != 0)
		{
			cout << "Enter the movie title: ";
			getline(cin, title);

			periodCheck = check(title);
			
			//Since there is no left aligned modifier I could find, I made one
			size = 35 - (genre.length() + title.length());
			spaces = space(size);
		}

		//Get Rating
		if (periodCheck != 0)
		{
			cout << "Enter movie rating between 1-5: ";
			rating = getNum();
		}

		if (periodCheck != 0)
		{
			//combine the genre and title into one key / value
			bothGenreTitle = genre + spaces + title;
				
			//Make two multimaps to print
			IntegerMultimap.insert(make_pair(rating, bothGenreTitle)); //Sorted by Rating
			StringMultimap.insert(make_pair(bothGenreTitle, rating)) ; //Sorted by Genre
		}

	}

	//Print the two multimaps
	printMultimap(IntegerMultimap, StringMultimap);

	//Second user input
	cout << "\nPlease enter a genre and title pair\n";

	cout << "Genre: ";
	getline(cin, genre);
	cout << "Title: ";
	getline(cin, title);

	size = 35 - (genre.length() + title.length());
	spaces = space(size);
	bothGenreTitle = genre + spaces + title;


	//deletion
	deletion(IntegerMultimap, StringMultimap, bothGenreTitle);

	//Ask to enter a new rating
	cout << "Enter the new movie rating between 1-5: ";
	rating = getNum();

	//Add the same title and genre with new rating
	IntegerMultimap.insert(make_pair(rating, bothGenreTitle));
	StringMultimap.insert(make_pair(bothGenreTitle, rating));

	//Print out the multimaps again
	printMultimap(IntegerMultimap, StringMultimap);
		
	return 0;
}




/*
 * Function: nTakeaway()
 * Description: This function takes away the \n at the end of the strings coming in from the user
 * Parameters: This function takes only 1 parameter which is the string that you want to take the
 *				\n away from
 * Return Values: This function returns the string
 */
const char* nTakeaway(char* str)
{
	int length = 0;
	length = (unsigned)strlen(str);

	str[length - 1] = '\0';

	return str;
}




/*
 * Function: getNum()
 * Description: This function gets a number from the user.
 * Parameters: Void / Nothing
 * Return Values: It returns the value that has been input by the user,
 *                the variable 'number'.
 */
int getNum(void)
{
	char record[121] = { 0 };
	int number = 0;

	fgets(record, 121, stdin);

	if (sscanf(record, "%d", &number) != 1)
	{
		number = -1;
	}
	return number;
}




/*
 * Function: check()
 * Description: This function takes in a string to see if it a period.
 * Parameters: string	word		the word to compare
 * Return Values: It returns 0 if it is a period
 *				  Or 1 if it is not a period
 */
int check(string word)
{
	if (word == ".")
	{
		return 0;
	}
	return 1;
}




/*
 * Function: space()
 * Description: This function returns a string that has an equal number of spaces in it to the int passed in.
 * Parameters: int	size		the amount of spaces wanted to be returned
 * Return Values: It returns a string with an equal number of spaces in it to the int size passed in
 */
string space(int size)
{
	string space;
	int i = 0;

	space = "";
	for (i = 0; i < size; i++)
	{
		space = space + " ";
	}

	return space;
}




/*
 * Function: printMultimap()
 * Description: This function prints everything in the multimaps passed in.
 * Parameters: The two parameters are both the multimaps used to print
 *				multimap<int, string>	IntegerMultimap			Sorted by integer/Rating
 *				multimap<string,int>	StringMultimap			Sorted by string/Genre
 * Return Values: Void / Nothing
 */
void printMultimap(multimap<int, string>IntegerMultimap, multimap<string,int> StringMultimap)
{
	multimap<int, string>::iterator posI;
	multimap<string, int>::iterator posS;
	
	for (posI = IntegerMultimap.begin(); posI != IntegerMultimap.end(); ++posI)
	{
		cout << posI->second << space(34) << posI->first << endl;
	}

	cout << endl;
	
	for (posS = StringMultimap.begin(); posS != StringMultimap.end(); ++posS)
	{
		cout << posS->first << space(34) << posS->second << endl;
	}
}




/*
 * Function: deletion()
 * Description: This function deletes a certain key and value pair based on the string passed in.
 * Parameters: There are 3 parameters, the 2 multimaps and a string which is the key value in the StringMultimap
 *				multimap<int, string>		IntegerMultimap		Sorted by integer/Rating
 *				multimap<string, int>		StringMultimap		Sorted by string/Genre
 *				string		bothGenreTitle		The string used to search
 * Return Values: It returns the value that has been input by the user,
 *                the variable 'number'.
 */
void deletion(multimap<int, string>IntegerMultimap, multimap<string, int> StringMultimap, string bothGenreTitle)
{
	int rating = 0;
	multimap<int, string>::iterator posI;
	multimap<string, int>::iterator posS;

	//Search for the key, since the user is not going to print two of the same genre and title
	//there are no duplicates
	for (posS = StringMultimap.begin(); posS != StringMultimap.end(); ++posS)
	{
		if (posS->first == bothGenreTitle)
		{
			cout << "Current movie rating is " << posS->second;
			//Get rating, since it is the key value for the other multimap
			rating = posS->second;
			cout << endl;
			break;
		}
	}
	//Delete the values at that iterator position
	StringMultimap.erase(posS);

	//Search for the key, but also for the values since there can be multiple 
	//movies with the same rating
	for (posI = IntegerMultimap.begin(); posI != IntegerMultimap.end(); ++posI)
	{
		if (posI->first == rating)
		{
			if (posI->second == bothGenreTitle)
			{
				break;
			}
		}
	}
	//Delete the values at that iterator position
	IntegerMultimap.erase(posI);
}