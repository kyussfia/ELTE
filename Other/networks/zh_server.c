#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <stdio.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

//#include "common.h"
#define TRUE 1

void printServerInfo(int sock);

int main()
{
    srand(time(0));
    int sock;
    struct sockaddr_in server;
    struct sockaddr_in client;
    int conn;
    int i;
    int variable;
    int encodedVar;
    char *randomproduct;
    int randomvariable;
    int len;
    socklen_t length;
    
    sock = socket( PF_INET, SOCK_STREAM, 0 );
    if ( sock == -1)
    {
	perror( "opening stream socket");
	exit(1);
    }
    server.sin_family = AF_INET;
    server.sin_addr.s_addr = INADDR_ANY;
    server.sin_port = 0;
    
    if( bind( sock, (struct sockaddr *) &server, sizeof server ) == -1 )
    {
	perror( "binding stream socket" );
	exit(2);
    }
    
    printServerInfo( sock);
    listen( sock, 5);
    printf("Listening...\n");
    
    int maxfds = sock;
    fd_set mysockets;
    fd_set tmpset;
    
    FD_ZERO( &mysockets );
    FD_SET( sock, &mysockets);
    
    do {
    
	tmpset = mysockets;
	
	select( maxfds +1, &tmpset, 0, 0, 0);
	if (FD_ISSET( sock, &tmpset))
	{
	    length = sizeof client;
	    
	    conn = accept( sock, (struct sockaddr *) &client, &length );
	    
	    FD_SET( conn, &mysockets );
	    maxfds = conn;
	    printf(" New client:%d\n", conn);
	}
	
	for (i =0; i<maxfds+1; ++i){
	    if (i!=sock && FD_ISSET( i, &tmpset))
	    {
		if(recv(i, &encodedVar, sizeof encodedVar, 0) > 0)
		{ 
		    variable = ntohl( encodedVar);
		    printf( "Kapott cikkszám: %d\n", variable);
		    randomvariable=rand();
		    printf( "Kliensnek küldött raktárkészlet: %d\n", randomvariable);
		    strcpy(randomproduct,"randomproduct");
		    printf( "Kliensnek küldött termék megnevezés: %s\n", randomproduct);
		    len = strlen(randomproduct);
		    encodedVar = htonl(randomvariable);
		    send( i, &encodedVar, sizeof encodedVar, 0);
		    send( i, randomproduct, len, 0);
		}    
		else
		{
		FD_CLR(i, &mysockets);
		close(i);
		printf(" Connection closed by client(%d)\n", i);
		}
	    }
	}
	} while( TRUE);
	
	exit(0);
	
}

void printServerInfo(int sock)
{
    struct sockaddr_in server_info;
    socklen_t length =sizeof server_info;
    
    if( getsockname(sock, (struct sockaddr *) &server_info, &length ) == -1)
    {
	perror( "getting socket name" );
	exit(3);
    }
    printf( "Server IP: %s\n", inet_ntoa( server_info.sin_addr ));
    printf( "Server port: %d\n", ntohs( server_info.sin_port));

}