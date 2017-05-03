select
ii.serial_number
, p.product_desc
, ii.item_cond
, iid.available_quantity
, iid.reorder_quantity
, case sl.location_id when 1 then 'St. Louis'
when 2 then 'St. Charles'
when 3 then 'Bridgeton'
when 4 then 'Kansas City'
ELSE 'Kirkwood' 
end as Store_Location
, iid.item_location_address
, i.is_discontinued 
from item_inventory ii
inner join
item_detail iid on
iid.item_detail_id = ii.item_detail_id
inner join
item i on
ii.item_detail_id = i.item_number
inner join product p on
p.product_code = i.product_code
inner join store_location sl on
sl.location_id = iid.location_id
;
