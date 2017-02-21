import 'jquery'
import homeController from './controllers/home-controller.js'
import gameController from './controllers/game-controller.js'

let navigo = new Navigo(null, false),
    mainContainer = $('#main');

homeController.loadEvents('body');
homeController.loadNavigation('header');

navigo.on(() => {
        gameController.loadSingleGame(mainContainer);
        homeController.home(mainContainer);
    })
    .resolve();