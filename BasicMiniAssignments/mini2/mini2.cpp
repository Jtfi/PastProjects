/*
 * Filename: mini2.cpp
 * By: Justin Fink
 * Date: January 29, 2021
 * Description: This function gets movie genres and titles
 *				from the user, then it uses malloc to put those
 *				into a struct list, then the program prints off all
 *				the genres and titles neatly using width specifiers.
 *
 */



#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#pragma warning(disable:4996)


//Structure
struct MovieInfo
{
	char movieGenre[42];
	char movieTitle[42];
	struct MovieInfo* next;
};




//Prototypes
MovieInfo* getMovieInfo(struct MovieInfo* newHead, char genre[], char title[]);
void printMovieInfo(struct MovieInfo* head);
const char* nTakeaway(char* str);
MovieInfo* freeHead(struct MovieInfo* newHead);




// constants
#define NUMBER_OF_MOVIES 10
#define STRING_LENGTH 36




int main()
{
	int periodCheck = 1;
	char genre[STRING_LENGTH] = {};
	char title[STRING_LENGTH] = {};
	struct MovieInfo* head = NULL;

	while (periodCheck != 0)
	{
		//promt user to input genre/movie
		printf("Enter the movie genre: ");
		fgets(genre, STRING_LENGTH, stdin);
		nTakeaway(genre);

		periodCheck = strcmp(genre, ".");

		if (periodCheck != 0)
		{
			printf("Enter the movie title: ");
			fgets(title, STRING_LENGTH, stdin);
			nTakeaway(title);
			periodCheck = strcmp(title, ".");
		}
	
		if (periodCheck != 0)
		{
			head = getMovieInfo(head, genre, title);
		}

	} 

	printMovieInfo(head);

	freeHead(head);

	return 0;
}




/*
 * Function: getMovieInfo()
 * Description: This function uses malloc to allocate the necessary size for the struct to then
 *				hold the movie genre and movie title, copying those into the struct also takes place
 *				here
 * Parameters: This function takes 3 parameters, a pointer to an element of the list
 *				the movie genre and movie title
 * Return Values: This function returns newHead for if something went wrong and for when it actually gets the data
 */
MovieInfo* getMovieInfo(struct MovieInfo* newHead, char genre[], char title[])
{
	MovieInfo* newBlock = NULL;
	MovieInfo* ptr = NULL;
	MovieInfo* prev = NULL;
	
	//Allocate the data
	newBlock = (MovieInfo*)malloc(sizeof(MovieInfo));
	if (newBlock == NULL)
	{
		printf("Out of memory\n");
		return newHead;
	}

	//Copy the Genre and Title into the structure
	strcpy(newBlock->movieGenre, genre);
	strcpy(newBlock->movieTitle, title);

	//Initilize next list
	newBlock->next = NULL;
	
	//Place the data in the right spots in th list
	if (newHead == NULL)
	{
		newHead = newBlock;
	}
	else
	{
		prev = newHead;
		ptr = newHead->next;

		while (ptr != NULL)
		{
			prev = ptr;
			ptr = ptr->next;
		}

		newBlock->next = ptr;
		prev->next = newBlock;
	}

	return newHead;
}




/*
 * Function: printMovieInfo()
 * Description: This function prints off the movie genres and titles one pair for
 *				each line using width specifiers to make sure its 35 characters apart
 * Parameters: This function takes only 1 parameter which is the structure of lists itself
 * Return Values: This function does not return anything
 */
void printMovieInfo(struct MovieInfo* head)
{
	MovieInfo* list = head;

	while (list != NULL)
	{
		printf("%-35s", list->movieGenre);
		printf("%s\n", list->movieTitle);
		list = list->next;
	}

}




/*
 * Function: freeHead()
 * Description: This function frees all the allocated data in the structure list
 *				it saves the previous list and then flips to the next and frees the last
 *				since if you were to just free a list there would be no way to go forward to 
 *				the next one.
 * Parameters: This function takes only 1 parameter which is the structre itself
 * Return Values: This function returns NULL to the function
 */
MovieInfo* freeHead(struct MovieInfo* head)
{
	MovieInfo* prev = NULL;
	MovieInfo* list = head;

	while (list != NULL)
	{
		prev = list;
		list = list->next;
		free(prev);
	}

	return NULL;
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
	length = strlen(str);

	str[length - 1] = '\0';

	return str;
}