import { BookResponse } from "../Book/book-response";
import { AuthorResponse } from "./author-response";

export interface AuthorBookResponse{
    author: AuthorResponse;
    writtenBooks: BookResponse[];
}