function starter() {
    var header = document.querySelector('header');
    var section = document.querySelector('section');

    var requestURL = 'https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json';
    var request = new XMLHttpRequest();
    request.open('GET', requestURL);
    request.responseType = 'text';
    request.send();
    request.onload = function () {
        var superHeroesText = request.response;
        var superHeroes = JSON.parse(superHeroesText);
        populateHeader(superHeroes);
        showHeroes(superHeroes);
    }

    function populateHeader(jsonObj) {
        var h1 = document.createElement('h1');
        h1.textContent = jsonObj['squadname']; //Creates new header element
        header.appendChild(h1);

        var para = document.createElement('p')
        para.textContent = 'Hometown: ' + jsonObj['homeTown'] + ' Formed: ' + jsonObj['formed'];
        header.appendChild(para); //Populates the header

    }

    function showHeroes(jsonObj) {
        var hero = jsonObj['members'];

        //Tests jsonObj
   



        for (var x = 0; x < hero.length; x++) {              //loops through each object
            var Article = document.createElement('article'); //creates new HTML element, paragraph, and list elements for superHero data
            var h2 = document.createElement('h2');
            var para1 = document.createElement('p');
            var para2 = document.createElement('p');
            var para3 = document.createElement('p');
            var List = document.createElement('ul');
            
            h2.textContent = hero[x].name;
            para1.textContent = 'Secret identity: ' + hero[x].secretIdentity;   //populates para elements
            para2.textContent = 'Age: ' + hero[x].age;
            para3.textContent = 'Superpowers:';
            var superPowers = hero[x].powers;
            for (var y = 0; y < superPowers.length; y++) {
                var listX = document.createElement('li');
                listX.textContent = superPowers[y];
                List.appendChild(listX);
            }

            Article.appendChild(h2);
            Article.appendChild(para1);
            Article.appendChild(para2);
            Article.appendChild(para3);
            Article.appendChild(List);
            section.appendChild(Article);

        }
    }
}