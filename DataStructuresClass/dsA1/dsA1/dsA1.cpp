#pragma once

#include <stdio.h>
#include <malloc.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#include "dsA1.h"

#pragma warning(disable:4996)



// constants
#define NUMBER_OF_MOVIES 10
#define STRING_LENGTH 36


int main()
{
	int counter = 0;
	int i = 0;
	int periodCheck = 1;

	char genre[STRING_LENGTH] = {};
	char title[STRING_LENGTH] = {};
	int rating = 0;

	struct MovieInfo* ratingHead = NULL;
	struct MovieInfo* ratingTail = NULL;

	struct MovieInfo* genreHead = NULL;
	struct MovieInfo* genreTail = NULL;

	struct MovieInfo* deleteMovie = NULL;

	//First user input
	while (periodCheck != 0)
	{
		//promt user to input genre/movie/rating
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
			printf("Enter movie rating between 1-5: ");
			rating = getNum();
		}


		//Doubly linked Lists sorting function
		if (periodCheck != 0)
		{
			ratingHead = getRatingList(&ratingHead, &ratingTail, genre, title, rating);
			genreHead = getGenreList(&genreHead, &genreTail, genre, title, rating);
		}
		
	}

	//1st Print
	printMovieInfo(ratingHead);

	printMovieInfo(genreHead);

	//Second user input
	printf("Please enter a genre and title pair\n");

	printf("Genre: ");
	fgets(genre, STRING_LENGTH, stdin);
	nTakeaway(genre);

	printf("\nTitle: ");
	fgets(title, STRING_LENGTH, stdin);
	nTakeaway(title);


	deleteMovie = findMovie(ratingHead, genre, title);

	if (deleteMovie != NULL)
	{
		printf("\nCurrent movie rating is %d", deleteMovie->movieRating);

		printf("\nEnter the new movie rating between 1-5: ");
		rating = getNum();

		printf("\n");

		if (rating != deleteMovie->movieRating)
		{
			//Updating genre lists rating value
			while(genreHead != NULL)
			{
				if (strcmp(genreHead->movieGenre, deleteMovie->movieGenre) == 0)
				{
					genreHead->movieRating = rating;
					break;
				}
				genreHead = genreHead->next;
				counter++;
			}

		
			for (i = 0; i < counter; i++)
			{
				genreHead = genreHead->prev;
			}
			
			
			

			//DeleteMovie
			deleteNode(deleteMovie, &ratingHead, &ratingTail);
			//Re-insert the data
			ratingHead = getRatingList(&ratingHead, &ratingTail, genre, title, rating);


		}

	}



	//2nd Print, with the change above
	printMovieInfo(ratingHead);
	printMovieInfo(genreHead);

	//Free ALL allocated memory
	freeHead(ratingHead);
	freeHead(genreHead);

	return 0;
}




