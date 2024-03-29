import { AuthorRequest } from "../Author/author-request";
import { AuthorResponse } from "../Author/author-response";
import { PublishingHouseRequest } from "../PublishingHouse/publishing-house-request";

export class BookRequest{
    constructor(private title: string, private authors: AuthorRequest[], private publishingHouse: PublishingHouseRequest) {}
}