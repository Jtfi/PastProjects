#pragma once

#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#include "dsA1.h"




#pragma warning(disable:4996)



/*
 * Function: getRatingList()
 * Description: This function uses malloc to allocate the necessary size for the struct to then
 *				hold the movie genre and movie title, copying those into the struct also takes place
 *				here
 * Parameters: This function takes 5 parameters, a pointer to the head and tail of the list,
 *				the movie genre, movie title and movie rating
 * Return Values: This function returns newHead for if something went wrong and for when it actually gets the data
 */
MovieInfo* getRatingList(struct MovieInfo** newRatingHead, struct MovieInfo** newRatingTail, char* genre, char* title, int rating)
{

	MovieInfo* newBlock = NULL;
	MovieInfo* beforeElement = NULL;

	MovieInfo* afterElement = NULL;


	//Allocate the data for a list
	newBlock = (MovieInfo*)malloc(sizeof(MovieInfo));
	if (newBlock == NULL)
	{
		printf("Out of memory\n");
		return *newRatingHead;
	}

	//Now allocate the genre, movie and rating
	

	//getting the movie genre
	newBlock->movieGenre = (char*)malloc(30 * sizeof(char*));
	if (newBlock->movieGenre == NULL)
	{
		printf("Out of memory\n");
		return *newRatingHead;
	}
	strcpy(newBlock->movieGenre, genre);



	//getting the movie title
	newBlock->movieTitle = (char*)malloc(30 * sizeof(char*));
	if (newBlock->movieTitle == NULL)
	{
		printf("Out of memory\n");
		return *newRatingHead;
	}
	strcpy(newBlock->movieTitle, title);
	

	//getting the rating
	newBlock->movieRating = (int)malloc(sizeof(int));
	if (newBlock->movieRating == NULL)
	{
		printf("Out of memory\n");
		return *newRatingHead;
	}

	newBlock->movieRating = rating;

	
	//Initilize next and the previous
	newBlock->prev = NULL;
	newBlock->next = NULL;


	//Place the data in the right spots in the list
	if (*newRatingHead == NULL)
	{
		*newRatingHead = newBlock;
		*newRatingTail = newBlock;
	}
	else if ((*newRatingHead)->movieRating >= rating)
	{
		newBlock->next = *newRatingHead;
		(*newRatingHead)->prev = newBlock;
		*newRatingHead = newBlock;
	}
	else
	{
		beforeElement = *newRatingHead;
		afterElement = (*newRatingHead)->next;
		while (afterElement != NULL)
		{
			if (afterElement->movieRating >= rating)
			{
				break;
			}

			beforeElement = afterElement;
			afterElement = afterElement->next;
		}

		newBlock->prev = beforeElement;
		newBlock->next = afterElement;
		beforeElement->next = newBlock;

		if (afterElement == NULL)
		{
			*newRatingTail = newBlock;
		}
		else
		{
			afterElement->prev = newBlock;
		}
	}
	return *newRatingHead;
}






/*
 * Function: getGenreList()
 * Description: This function uses malloc to allocate the necessary size for the struct to then
 *				hold the movie genre and movie title, copying those into the struct also takes place
 *				here
 * Parameters: This function takes 5 parameters, a pointer to the head and tail of the list,
 *				the movie genre, movie title and movie rating
 * Return Values: This function returns newHead for if something went wrong and for when it actually gets the data
 */
MovieInfo* getGenreList(struct MovieInfo** newGenreHead, struct MovieInfo** newGenreTail, char* genre, char* title, int rating)
{

	MovieInfo* newBlock2 = NULL;
	MovieInfo* beforeElement = NULL;

	MovieInfo* afterElement = NULL;
	
	//Allocate the data for a list
	newBlock2 = (MovieInfo*)malloc(sizeof(MovieInfo));
	if (newBlock2 == NULL)
	{
		printf("Out of memory\n");
		return *newGenreHead;
	}

	//Now allocate the genre, movie and rating


	//getting the movie genre
	newBlock2->movieGenre = (char*)malloc(30 * sizeof(char*));
	if (newBlock2->movieGenre == NULL)
	{
		printf("Out of memory\n");
		return *newGenreHead;
	}
	strcpy(newBlock2->movieGenre, genre);



	//getting the movie title
	newBlock2->movieTitle = (char*)malloc(30 * sizeof(char*));
	if (newBlock2->movieTitle == NULL)
	{
		printf("Out of memory\n");
		return *newGenreHead;
	}
	strcpy(newBlock2->movieTitle, title);


	//getting the rating
	newBlock2->movieRating = (int)malloc(sizeof(int));
	if (newBlock2->movieRating == NULL)
	{
		printf("Out of memory\n");
		return *newGenreHead;
	}

	newBlock2->movieRating = rating;
	

	newBlock2->prev = NULL;
	newBlock2->next = NULL;


	//Place the data in the right spots in the list
	if (*newGenreHead == NULL)
	{
			*newGenreHead = newBlock2;
			*newGenreTail = newBlock2;
	}
	else if (strcmp((*newGenreHead)->movieGenre, genre) >= 0)
	{
		newBlock2->next = *newGenreHead;
		(*newGenreHead)->prev = newBlock2;
		*newGenreHead = newBlock2;
	}
	else
	{
		beforeElement = *newGenreHead;
		afterElement = (*newGenreHead)->next;
		while (afterElement != NULL)
		{
			if (strcmp(afterElement->movieGenre, genre) >= 0)
			{

				break;
			}
			beforeElement = afterElement;
			afterElement = afterElement->next;
		}
		newBlock2->prev = beforeElement;
		newBlock2->next = afterElement;
		beforeElement->next = newBlock2;


		if (afterElement == NULL)
		{
			*newGenreTail = newBlock2;
		}
		else
		{
			afterElement->prev = newBlock2;
		}
	}
	return *newGenreHead;
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
	//Print all the nodes data in the rating sorted list
	MovieInfo* sortedList = head;
	while (sortedList != NULL)
	{
		printf("%-35s", sortedList->movieGenre);
		printf("%-35s", sortedList->movieTitle);
		printf("%d\n", sortedList->movieRating);
		sortedList = sortedList->next;
	}

	printf("\n");

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
 * Function: findMovie()
 * Description: This function compares the title and the genre to see if the nodes match and have the same data in them
 *				if it does it returns the nod
 * Parameters: struct MovieInfo * head,  this is the head of the node
 *				char* genre,  this is the genre you want to compare to the ones in the nodes
 *				char* title,  this is the title you want to compare to the ones in the nodes
 * Return Values: It returns either the node that has the match or NULL if it did not find it
 */
MovieInfo* findMovie(struct MovieInfo* head, char* genre, char* title)
{
	MovieInfo* nodeList = head;

	while (nodeList != NULL)
	{
		
		if ((strcmp(nodeList->movieGenre, genre) == 0) && (strcmp(nodeList->movieTitle, title) == 0))
		{
			return nodeList;
		}

		nodeList = nodeList->next;
	}

	return NULL;
}




/*
 * Function: deleteNode()
 * Description: This function deletes a node and uses pointers to correct the list path
 * Parameters: Void / Nothing
 * Return Values: It returns nothing
 */
void deleteNode(struct MovieInfo* node,struct MovieInfo** head,struct MovieInfo** tail)
{
	int checker = 0;
	int otherChecker = 0;
	MovieInfo* curr = NULL;
	MovieInfo* next = NULL;

	curr = *head;

	while (curr != NULL)
	{

		if (strcmp(node->movieGenre, curr->movieGenre) == 0)
		{
			
			if ((curr == *head) && (curr == *tail))
			{
				*head = *tail = NULL;
				free(curr);
				checker = 1;
				otherChecker = 1;
			}

			if (checker == 0)
			{

				if (curr == *head)
				{
					MovieInfo* secondElement = curr->next;
					*head = curr->next;
					secondElement->prev = NULL;
				}
				else
				{
					
					if (curr == *tail)
					{
						MovieInfo* secondlastElement = curr->prev;
						*tail = curr->prev;
						secondlastElement->next = NULL;
					}
					else
					{
						MovieInfo* precedingElement = curr->prev;
						MovieInfo* followingElement = curr->next;
						followingElement->prev = precedingElement;
						precedingElement->next = followingElement;
					}
				}


			}

			if (otherChecker == 0)
			{
				free(curr);
			}
				
			
			

			checker = 1;
			break;
		}


		curr = curr->next;
		
		if (checker == 1)
		{
			break;
		}

	}


}