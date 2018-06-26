#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
//#include "common.h"

int main (argc, argv)
    int argc;
    char *argv[];
{
    int sock;
    struct sockaddr_in server;
    struct hostent *hp;
    char buff[1024];
    int i;
    int pnumber;
    int encodedVar;
    int variable;
    int rval;
    
    if (argc<3)
    {
	printf("Usage:\n%s <server's hostname> <port>", argv[0]);
	exit(-1);
    }
    
    sock = socket (AF_INET, SOCK_STREAM, 0);
    if (sock == -1)
    {
	perror( "opening stream socket");
	exit(1);
    }
    
    server.sin_family = AF_INET;
    hp = gethostbyname( argv[1] );
    if ( hp == (struct hostent *) 0 )
    {
	fprintf( stderr, "%s: unknown host \n", argv[1] );
	exit(2);
    }
    
    memcpy( (void *) &server.sin_addr, (void *) hp->h_addr, hp->h_length);
    
    server.sin_port = htons( atoi (argv[2] ) );
    
    if( connect(sock, (struct sockaddr *) &server, sizeof server) == -1)
    {
	perror( "connecting stream socket");
	exit(3);
    }
    
    printf("Kérek egy cikkszámot: ");
    scanf("%d", &pnumber);
    encodedVar = htonl( pnumber);
    
    
    for (i=0;i<5;++i)
    {
	if( send( sock, &encodedVar, sizeof encodedVar, 0) == -1)
	{
	    perror("sending product number");
	}
	
	if (( rval = recv( sock, buff, sizeof buff, 0)) == -1)
	{
	    perror( "reading stream socket" );
	}
	else
	{
	    buff[rval] = '\0';
	    printf(" Termékmegnevezés: %s\n", buff);
	}
	
	if ( recv( sock, &encodedVar, sizeof encodedVar, 0) == -1)
	{
	    perror( "receiving a variable");
	}
	
	variable = ntohl(encodedVar);
	printf("Raktárkészlet: %d\n", variable);
	
	printf("Kérek egy cikkszámot: ");
	scanf("%d", &pnumber);
	encodedVar = htonl( pnumber);
	sleep(2);
    }
    
    close(sock);
    
    exit(0);
    
}