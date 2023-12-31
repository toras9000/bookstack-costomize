<?php

use BookStack\Facades\Theme;
use BookStack\Theming\ThemeEvents;
use BookStack\Entities\Controllers;
use BookStack\Entities\Models;
use Illuminate\Routing\Router;

// Register a permalink endpoint
Theme::listen(ThemeEvents::ROUTES_REGISTER_WEB, function (Router $router)
{
    $router->get('/link/shelves/{id}', function (string $id)
    {
        $entity = Models\Bookshelf::query()->where('id', '=', $id)->first();
        return isset($entity) ? redirect($entity->getUrl()) : abort(404);
    })->whereNumber('id');

    $router->get('/link/books/{id}', function (string $id)
    {
        $entity = Models\Book::query()->where('id', '=', $id)->first();
        return isset($entity) ? redirect($entity->getUrl()) : abort(404);
    })->whereNumber('id');

    $router->get('/link/chapters/{id}', function (string $id)
    {
        $entity = Models\Chapter::query()->where('id', '=', $id)->first();
        return isset($entity) ? redirect($entity->getUrl()) : abort(404);
    })->whereNumber('id');

    $router->get('/link/pages/{id}', function (string $id)
    {
        $entity = Models\Page::query()->where('id', '=', $id)->first();
        return isset($entity) ? redirect($entity->getUrl()) : abort(404);
    })->whereNumber('id');
});
