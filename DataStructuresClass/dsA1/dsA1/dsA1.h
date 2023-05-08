/*
 * Filename: dsA1
 * By: Justin Fink
 * Date: February,26 2021
 * Description: This function gets movie genres, titles and ratings
 *				from the user, then it uses malloc to put those
 *				into two sorted doubly linked struct list, then the program prints off all
 *				the genres and titles neatly using width specifiers. This program also uses 
 *				deleteNode function to delete a node and correct the pointer path.
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
	char* movieGenre;
	char* movieTitle;
	int movieRating;
	struct MovieInfo* prev;
	struct MovieInfo* next;
};

//Prototypes
MovieInfo* getRatingList(struct MovieInfo** newRatingHead, struct MovieInfo** newRatingTail, char* genre, char* title, int rating);
MovieInfo* getGenreList(struct MovieInfo** newGenreHead, struct MovieInfo** newGenreTail, char* genre, char* title, int rating);
void printMovieInfo(struct MovieInfo* head);


const char* nTakeaway(char* str);
MovieInfo* freeHead(struct MovieInfo* newHead);


int getNum(void);
MovieInfo* findMovie(struct MovieInfo* head, char* genre, char* title);
void deleteNode(struct MovieInfo* node,struct MovieInfo** head,struct MovieInfo** tail);