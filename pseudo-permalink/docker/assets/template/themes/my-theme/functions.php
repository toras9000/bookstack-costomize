<?php

use BookStack\Facades\Theme;
use BookStack\Theming\ThemeEvents;
use BookStack\Entities\Models\Bookshelf;
use BookStack\Entities\Models\Book;
use BookStack\Entities\Models\Chapter;
use BookStack\Entities\Models\Page;
use Illuminate\Http\Request;

Theme::listen(ThemeEvents::WEB_MIDDLEWARE_BEFORE , function(Request $request)
{
    $req_path = $request->path();
    if (preg_match('!^/?link/(shelves|books|chapters|pages)/(\d+)$!', $req_path,  $matches))
    {
        $req_category = $matches[1];
        $req_id = $matches[2];
        switch ($req_category)
        {
        case 'shelves':  $entity = Bookshelf::query()->where('id', '=', $req_id)->first(); break;
        case 'books':    $entity = Book::query()->where('id', '=', $req_id)->first();      break;
        case 'chapters': $entity = Chapter::query()->where('id', '=', $req_id)->first();   break;
        case 'pages':    $entity = Page::query()->where('id', '=', $req_id)->first();      break;
        }
        if (isset($entity))
        {
            return redirect($entity->getUrl());
        }
    }
});
