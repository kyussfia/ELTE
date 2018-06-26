//			WEB server
//			TCP kapcsolatokat fogado server

#include<stdio.h>
#include<stdlib.h>
#include<unistd.h>
#include<sys/types.h>
#include<netinet/in.h>
#include<netdb.h>
#include<sys/socket.h>
#include<fcntl.h>
#include<sys/stat.h>
#include<sys/wait.h>
#include<signal.h>
#include<string.h>


#define ROOTDIR		"	/h2/public/tanulok/proginf2008/migoaai/public_html"
#define PORT			8080

char 					oldal_neve[1024];;
void					*buffer;
size_t				meret;
int					kapcsolat;


//az oldal_neve nevu fajlt betolti a bufferba, a buffernek kello meretu helyet foglalva, amelyhez a fajl meretet hasznalja
void fajlt_betolt() {
	struct stat		st;
	int			fajl;

	stat( oldal_neve, &st );

	meret = st.st_size;
	buffer = malloc( meret );

	fajl = open( oldal_neve, O_RDONLY );
	read( fajl, buffer, meret );
	close( fajl );
}


//letrehozza a kapcsolartot
int kapcsolat_letrehozasa(){
	int				kapcsolat;
	struct sockaddr_in 	cim;

	cim.sin_family		= AF_INET;
	cim.sin_port		= htons( PORT );
	cim.sin_addr.s_addr 	= htonl( INADDR_ANY);	//barkinek a cimet fogadja

	kapcsolat = socket( PF_INET, SOCK_STREAM, 0 );		//STREAM jellegu kapcsolat (TCP)
	bind(	kapcsolat, (struct sockaddr *) &cim, sizeof( cim ) );

	return kapcsolat;
}



int olvasas( int kapcsolat ) {
	char		tmp[1024];
	int		n;

	sprintf( oldal_neve, ROOTDIR );		//konyvtar bemasolasa a fajlnev elejere
	n = read( kapcsolat, tmp, 1024 );		//a kert fajl nevenek a beolvasasa
	sscanf( tmp, "GET %s", &oldal_neve[ strlen( ROOTDIR ) ] );		//a fajl nevet beirjuk az oldal neve tombbe

	return n;
}


int iras( int kapcsolat ) {
	char head[] = "HTTP/1.0 200 OK\r\n\
	Server: proba/0.1\r\n\
	Connection> close\r\n\
	Contetn-Type: text/html\r\n\r\n";

	write( kapcsolat, head, strlen(head) );		//a fejlec kiirasa (elkuldese)
	write( kapcsolat, buffer, meret );			//a buffer kiirasa (ebben van a kert fajl)
	return 0;
}


void mukodtet() {
	int 				uj_kapcsolat;
	struct sockaddr_in	client;
	size_t			size;

	kapcsolat = kapcsolat_letrehozasa();
	listen( kapcsolat, 5 );

	while(1) {

		size = sizeof( client );
		uj_kapcsolat = accept( kapcsolat, (struct sockaddr *) &client, &size );		//uj kapcsolatra varakozas

		if (fork() == 0 ) {					//ha jott keres fork-olunk, majd a gyerek kiszolgalja a kerest, a szulo var uj kapcsolatra
			olvasas( uj_kapcsolat );
			fajlt_betolt();
			iras( uj_kapcsolat );
			shutdown( uj_kapcsolat, SHUT_RDWR );		//a kapcsolat bontasa
			free( buffer );
			return;
		}
	}
}


void signal_kezelo( int uzenet ) {
	switch( uzenet ) {
		case SIGCHLD:
			while( waitpid( WAIT_ANY, NULL, WNOHANG )>0 ) ;		//a zombik ellen
			break;
	}
}



int main() {
	daemon(0,0);
	signal( SIGCHLD, signal_kezelo );
	mukodtet();
	return 0;
}
